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

    void PopulateBoard()
    {
        GameObject _env = GameObject.FindGameObjectWithTag("BoardTag");
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _grid = new List<GameObject>();
        int offset = 2;

        tile.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject sqr = Instantiate(tile, _env.transform);
                sqr.transform.position = new Vector3(offset * i, 0.0f, offset * j);
                sqr.transform.localScale = offset * Vector3.one;
                _grid.Add(sqr);
            }
        }
        Destroy( tile );
    }
}
