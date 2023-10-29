using UnityEngine;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    //! Places a new card, if the tile is free
    private void OnMouseDown()
    {
        if (placedCard)
            return;

        GameObject card = GameManager.Instance.GetPlayerCard();

        if (card == null)
            return;

        DisplayCard(card);
        GameManager.Instance.DrawRandomCard();
    }

    //! Spawns a card on itself
    public void DisplayCard(GameObject card)
    {
        placedCard = card;
        placedCard.GetComponent<CardScript>().Spawn(card, transform.position + Vector3.up * 0.01f);
    }

    private void OnMouseEnter()
    {
        _meshRenderer.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        _meshRenderer.material.color = Color.white;
    }
}
