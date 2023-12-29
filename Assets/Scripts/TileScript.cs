using UnityEngine;
using UnityEngine.AI;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer meshRenderer;
    private NavMeshSurface surface;
    private TileScript neighbour;

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
        if (placedCard || GameManager.Instance.BombsSelected || !neighbour || neighbour.placedCard)
        {
            return;
        }

        placedCard = GameManager.Instance.GetPlayerCard();

        if (placedCard == null)
        {
            return;
        }

        placedCard = Instantiate(placedCard);
        neighbour.placedCard = placedCard;
        
        CardData data = placedCard.GetComponent<CardScript>().Data;
        placedCard.name = $"Card_{data.Color}_{data.Value}";
        placedCard.transform.parent = gameObject.transform;

        CardScript cs = placedCard.GetComponent<CardScript>();
        BoardScript bs = FindObjectOfType<BoardScript>();
        Vector3 adjustedPosition = transform.position + Vector3.up * transform.localScale.y / 2.0f + new Vector3(
            transform.localScale.x * bs.PlacementDirection.z / 2.0f,
            0.0f,
            transform.localScale.z * bs.PlacementDirection.x / 2.0f
        );
        cs.PlaceBuilding(adjustedPosition, bs.PlacementRotation);
        cs.PlaceFountain(adjustedPosition, bs.PlacementRotation);
        cs.PlaceTrees(adjustedPosition, bs.PlacementRotation);
        cs.SpawnSecret(adjustedPosition);
        
        foreach(var property in cs.Data.Parameters)
        {
            GameManager.Instance.GameParameters[property.Category] += property.Value;
        }

        SpawnPedestrians();
        surface.BuildNavMesh();

        GameHUDManager.Instance.UpdateUI();
    }

    //! Spawn pedestrians, when a building is placed
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

    //! Colors plane to the given color
    private void ColorPlane(Color color)
    {
        meshRenderer.material.color = color;
    }

    private void OnMouseEnter()
    {
        Color color = GameManager.Instance.BombsSelected ? Color.red : Color.cyan;
        ColorPlane(color);

        TileScript neighbourTile = FindObjectOfType<BoardScript>().GetNeighbour(this)?.GetComponent<TileScript>();
        if(neighbourTile == null)
        {
            neighbour = null;
            Debug.Log("Reaching out of the grid");

            return;
        }

        neighbour = neighbourTile;
        neighbour.ColorPlane(color);
    }

    private void OnMouseExit()
    {
        ColorPlane(Color.white);

        if(neighbour != null)
        {
            neighbour.ColorPlane(Color.white);
        }
    }
}
