using System.Collections.Generic;
using UnityEngine;

//! Class responsible for creating a board
public class BoardScript : MonoBehaviour
{
    private Vector3 boardCenter;

    //! Board's center position
    public Vector3 BoardCenter => boardCenter;
    
    //! Dimension X of the board
    [SerializeField] private int _width  = 2;
    //! Dimension Y of the board
    [SerializeField] private int _height = 2;
    
    private Vector3 boardSize;
    private Vector3 tileSize;
    private GameObject tilePrefab;
    private List<GameObject> _grid;

    private Vector3 placementDirection;
    public Vector3 PlacementDirection => placementDirection;

    private Quaternion placementRotation;
    public Quaternion PlacementRotation => placementRotation;

    private int directionsIdx = 0;
    private readonly Vector3[] directions =
    {
        new( 1.0f, 0.0f,  0.0f),
        new( 0.0f, 0.0f,  1.0f),
        new(-1.0f, 0.0f,  0.0f),
        new( 0.0f, 0.0f, -1.0f)
    };
    private readonly Quaternion[] rotations =
    {
        Quaternion.Euler( 0.0f, 0.0f,   0.0f ),
        Quaternion.Euler( 0.0f, 90.0f,  0.0f ),
        Quaternion.Euler( 0.0f, 180.0f, 0.0f ),
        Quaternion.Euler( 0.0f, 270.0f, 0.0f )
    };

    //! Spawns collider walls around board
    public void SpawnCollider()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new(boardCenter.x - _width * tileSize.x / 2.0f, 0.0f, 0.0f);
        collider.size = new(1.0f, 1000.0f, boardSize.z);
        
        collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new(boardCenter.x + _width * tileSize.x / 2.0f, 0.0f, 0.0f);
        collider.size = new(1.0f, 1000.0f, boardSize.z);
        
        collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new(0.0f, 0.0f, boardCenter.z - _height * tileSize.z / 2.0f);
        collider.size = new(boardSize.x, 1000.0f, 1.0f);
        
        collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new(0.0f, 0.0f, boardCenter.z + _height * tileSize.z / 2.0f);
        collider.size = new(boardSize.x, 1000.0f, 1.0f);

    }

    //! Removes collider walls around board
    public void RemoveCollider()
    {
        BoxCollider[] colliders = GetComponents<BoxCollider>();

        foreach(BoxCollider collider in colliders)
        {
            Destroy(collider);
        }
    }
    
    public GameObject? GetNeighbour(TileScript caller)
    {
        int callerIdx = _grid.IndexOf(caller.gameObject);
        int callerW = callerIdx % _width;
        int callerH = callerIdx / _width;

        int neighbourIdx = callerIdx + (int)placementDirection.z * _width + (int)placementDirection.x;
        int neighbourW = neighbourIdx % _width;
        int neighbourH = neighbourIdx / _width;

        if(neighbourIdx < 0 || neighbourIdx >= _grid.Count
            || (callerW != neighbourW && callerH != neighbourH))
        {
            return null;
        }

        return _grid[neighbourIdx];
    }

    //! Loads data, populates board
    void Start()
    {
        tilePrefab = Resources.Load<GameObject>("Prefabs/BoardTile");
        _grid = new List<GameObject>();
        
        placementDirection = directions[directionsIdx];
        placementRotation = rotations[directionsIdx];

        directionsIdx = (directionsIdx + 1) % directions.Length;

        PopulateBoard();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            placementDirection = directions[directionsIdx];
            placementRotation = rotations[directionsIdx];
            directionsIdx = (directionsIdx + 1) % directions.Length;
        }
    }

    /**
     * Populates the board with set WIDTHxHEIGHT dimension
     */
    private void PopulateBoard()
    {
        GameObject _env = GameObject.FindGameObjectWithTag("BoardTag");

        float sizeX = tilePrefab.transform.localScale.x;
        float sizeZ = tilePrefab.transform.localScale.z;

        float offsetX = _width * sizeX / 2.0f;
        float offsetZ = _height * sizeZ / 2.0f;

        Vector3 pivotOffset = new Vector3(sizeX, 0.0f, sizeZ) / 2.0f;
        
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                GameObject sqr = Instantiate(tilePrefab, _env.transform);
                sqr.transform.position = new Vector3(sizeX * i - offsetX, 0.0f, sizeZ * j - offsetZ) + pivotOffset;
                sqr.name = $"Tile_{i}_{j}";
                _grid.Add(sqr);
            }
        }

        tileSize = tilePrefab.transform.localScale;
        tileSize.y = 0.0f;
        boardCenter = Vector3.zero;
        boardSize = new(_width * sizeX, 2000.0f, _height * sizeZ);
    }
}
