using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    [SerializeField] private int _width  = 2;
    [SerializeField] private int _height = 2;

    private List<GameObject> _grid;

    void Start()
    {
        PopulateBoard();
    }
    
    void Update()
    {
        
    }

    private void PopulateBoard()
    {
        GameObject _env = GameObject.FindGameObjectWithTag("BoardTag");
        GameObject tile = Resources.Load<GameObject>("Prefabs/Tile");
        _grid = new List<GameObject>();
        int offset = 2;

        tile.transform.localScale = offset * Vector3.one;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameObject sqr = Instantiate(tile, _env.transform);
                sqr.transform.position = new Vector3(offset * i, 0.0f, offset * j);
                sqr.name = $"Tile_{i}_{j}";
                _grid.Add(sqr);
            }
        }
    }
}
