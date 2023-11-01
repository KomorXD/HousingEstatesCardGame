using UnityEngine;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer meshRenderer;

    public void ClearTile()
    {
        if (placedCard == null)
        {
            return;
        }

        placedCard.GetComponent<CardScript>().Despawn();
        DestroyImmediate(placedCard, true);
        placedCard = null;
    }

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

        placedCard = GameManager.Instance.GetPlayerCard();

        if (placedCard == null)
        {
            return;
        }

        placedCard = Instantiate(placedCard);
        
        CardData data = placedCard.GetComponent<CardScript>().Data;
        placedCard.name = $"Card_{data.Color}_{data.Value}";
        placedCard.transform.parent = gameObject.transform;
        placedCard.GetComponent<CardScript>().PlaceBuilding(transform.position + Vector3.up * transform.localScale.y / 2.0f);
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
