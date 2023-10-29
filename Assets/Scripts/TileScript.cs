using UnityEngine;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //! Places a new card, if the tile is free
    private void OnMouseDown()
    {
        // Trying to place a card on top of another
        if (placedCard)
        {
            return;
        }

        if (GameManager.Instance.SelectedCard == null)
        {
            return;
        }

        // Putting a card on an empty slot
        CardData cardData = GameManager.Instance.SelectedCard.Value;
        GameObject card = GameManager.Instance.GetPlayerCard();

        GameObject env = GameObject.FindGameObjectWithTag("BoardTag");
        GameObject cardObj = Instantiate(card, env.transform);

        cardObj.name = $"Card_{cardData.Color}_{cardData.Value}";
        cardObj.GetComponent<CardScript>().Init(cardData);

        DisplayCard(cardObj);
    }

    //! Spawns a card on itself
    public void DisplayCard(GameObject card)
    {
        placedCard = card;
        placedCard.SetActive(false);
        placedCard.GetComponent<CardScript>().Spawn(card, transform.position + Vector3.up * 0.01f);
    }

    private void OnMouseEnter()
    {
        Color color = GameManager.Instance.BombsSelected ? Color.red : Color.cyan;

        meshRenderer.material.color = color;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = Color.white;
    }
}
