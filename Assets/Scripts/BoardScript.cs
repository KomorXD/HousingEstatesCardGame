using System.Collections.Generic;
using UnityEngine;

//! Class responsible for creating a board
public class BoardScript : MonoBehaviour
{
    //! Dimension X of the board
    [SerializeField] private int _width  = 2;
    //! Dimension Y of the board
    [SerializeField] private int _height = 2;

    private List<GameObject> _grid;

    void Start()
    {
        PopulateBoard();
    }

    /**
     * Populates the board with set WIDTHxHEIGHT dimension
     */
    private void PopulateBoard()
    {
        GameObject _env = GameObject.FindGameObjectWithTag("BoardTag");
        GameObject tile = Resources.Load<GameObject>("Prefabs/BoardTile");
        _grid = new List<GameObject>();

        float sizeX = tile.transform.localScale.x;
        float sizeZ = tile.transform.localScale.z;

        float offsetX = _width * sizeX / 2.0f;
        float offsetZ = _height * sizeZ / 2.0f;
        
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameObject sqr = Instantiate(tile, _env.transform);
                sqr.transform.position = new Vector3(sizeX * i - offsetX, 0.0f, sizeZ * j - offsetZ);
                sqr.name = $"Tile_{i}_{j}";
                _grid.Add(sqr);
            }
        }
    }
}
