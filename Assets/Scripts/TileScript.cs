using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//! Class responsible for interacting with each of the tile
public class TileScript : MonoBehaviour
{
    private GameObject placedCard;
    private MeshRenderer meshRenderer;
    private NavMeshSurface surface;
    private TileScript neighbour;
    private Color originalColor;

    private ParameterCategory? bonusParameter;
    public ParameterCategory? BonusParam => bonusParameter;

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

        if (GameManager.Instance.AvailableBombs <= 0)
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

        if(Random.Range(0.0f, 1.0f) > 0.9f)
        {
            bonusParameter = (ParameterCategory)Random.Range(1, 6);
            originalColor.r = (int)bonusParameter & 1;
            originalColor.g = (int)bonusParameter & 2;
            originalColor.b = (int)bonusParameter & 4;
            originalColor.a = 1.0f;
            ColorPlane(originalColor);

            return;
        }

        originalColor = Color.white;
        ColorPlane(originalColor);
        bonusParameter = null;
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

        // Xddd
        TileScript neighbourTile = bs.GetNeighbour(this)?.GetComponent<TileScript>();
        TileScript[] neighbourTiles = bs.GetNeighoursAround(this).Select(tile => tile.GetComponent<TileScript>()).ToArray();
        TileScript[] neighbourNeighbourTiles = bs.GetNeighoursAround(neighbourTile).Select(tile => tile.GetComponent<TileScript>()).ToArray();

        foreach (var property in cs.Data.Parameters)
        {
            float value = property.Value;

            if(bonusParameter != null && bonusParameter == property.Category)

            {
                value *= 2.0f;
            }

            if(neighbourTile != null && neighbourTile.bonusParameter == property.Category)
            {
                value *= 2.0f;
            }


            foreach(TileScript ts in neighbourTiles)
            {
                if(ts.placedCard != neighbourTile.placedCard && ts.placedCard.GetComponent<CardScript>().Data.PreferedNeighbour == property.Category)
                {
                    value *= 1.25f;
                }
            }

            foreach(TileScript ts in neighbourNeighbourTiles)
            {
                if(ts.placedCard != neighbourTile.placedCard && ts.placedCard.GetComponent<CardScript>().Data.PreferedNeighbour == property.Category)
                {
                    value *= 1.25f;
                }
            }

            GameManager.Instance.GameParameters[property.Category] += value;
        }
        // end of Xdd

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
        if (neighbourTile == null)
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
        ColorPlane(originalColor);

        if (neighbour != null)
        {
            neighbour.ColorPlane(neighbour.originalColor);
        }
    }
}
