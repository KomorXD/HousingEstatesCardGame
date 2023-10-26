using UnityEngine;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject _card;
    private Vector3 _cardPosition;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _cardPosition = transform.position;
        _cardPosition.y += 0.01f;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    //! Places a new card, if the tile is free
    private void OnMouseDown()
    {
        if (_card)
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
        _card = card;
        _card.GetComponent<CardScript>().Spawn(card, transform.position);
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
