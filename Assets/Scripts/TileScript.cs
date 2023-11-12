using UnityEngine;
using UnityEngine.AI;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer meshRenderer;
    private NavMeshSurface surface;

    //! Removes building from a tile, removes card's points from player
    public void ClearTile()
    {
        if (placedCard == null)
        {
            return;
        }

        CardScript cs = placedCard.GetComponent<CardScript>();

        foreach (var property in cs.Data.Parameters)
        {
            GameManager.Instance.GameParameters[property.Category] -= property.Value;
        }

        GameManager.Instance.AvailableBombs--;

        if(GameManager.Instance.AvailableBombs <= 0)
        {
            GameHUDManager.Instance.SetBombsInteractive(false);
            GameManager.Instance.BombsSelected = false;
        }

        cs.Despawn();
        DestroyImmediate(placedCard, true);
        placedCard = null;

        surface.BuildNavMesh();

        GameHUDManager.Instance.UpdateUI();
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        surface = FindObjectOfType<BoardScript>().gameObject.GetComponent<NavMeshSurface>();
    }

    //! Places a new card, if the tile is free
    private void OnMouseDown()
    {
        // Trying to place a card on top of another
        if (placedCard || GameManager.Instance.BombsSelected)
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

        CardScript cs = placedCard.GetComponent<CardScript>();
        cs.PlaceBuilding(transform.position + Vector3.up * transform.localScale.y / 2.0f);
        
        foreach(var property in cs.Data.Parameters)
        {
            GameManager.Instance.GameParameters[property.Category] += property.Value;
        }

        SpawnPedestrians();
        surface.BuildNavMesh();

        GameHUDManager.Instance.UpdateUI();
    }

    private void SpawnPedestrians()
    {
        GameObject pedestrianPrefab = Resources.Load<GameObject>("Prefabs/Pedestrian");
        Vector3 flatScale = new(transform.localScale.x, 0.0f, transform.localScale.z);
        Vector3 offsetPos = transform.position - flatScale / 2.0f;

        GameObject pedestrian = Instantiate(pedestrianPrefab);
        pedestrian.transform.position = offsetPos + Vector3.right * flatScale.x;

        pedestrian = Instantiate(pedestrianPrefab);
        pedestrian.transform.position = offsetPos + Vector3.forward * flatScale.z;

        pedestrian = Instantiate(pedestrianPrefab);
        pedestrian.transform.position = offsetPos + Vector3.right * flatScale.x + Vector3.forward * flatScale.z;

        pedestrian = Instantiate(pedestrianPrefab);
        pedestrian.transform.position = offsetPos;
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
