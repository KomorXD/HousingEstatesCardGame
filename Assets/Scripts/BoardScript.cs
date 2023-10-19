using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    [SerializeField] private int _width  = 2;
    [SerializeField] private int _height = 2;

    private List<GameObject> _grid;

    void Start()
    {
        GameObject sqr = GameObject.CreatePrimitive(PrimitiveType.Quad);
        sqr.transform.position = new Vector3(0.5f, 0.0f, 0.5f);
        sqr.transform.localScale = Vector3.one;
        sqr.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        _grid = new List<GameObject>
        {
            sqr
        };
    }
    
    void Update()
    {
        
    }
}
