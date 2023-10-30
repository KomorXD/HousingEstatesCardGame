using UnityEngine;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    // public GameObject placedCard;

    public GameObject card;
    public GameObject placedBuilding;
    
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //! Places a new card, if the tile is free
    private void OnMouseDown()
    {
        // Trying to place a card on top of another
        if (placedBuilding)
        {
            return;
        }

        card = GameManager.Instance.GetPlayerCard();

        if (card == null)
        {
            return;
        }

        placedBuilding = card.GetComponent<CardScript>().PlaceBuilding(transform.position);
        placedBuilding.transform.parent = gameObject.transform;
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
