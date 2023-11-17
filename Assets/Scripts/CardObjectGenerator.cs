using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CardObjectGenerator : MonoBehaviour
{
    public List<Vector3> fountainPositions = new List<Vector3>();

    public Vector3 GetFountainPosition()
    {
        fountainPositions.Add(new Vector3(25, 0, 60));
        fountainPositions.Add(new Vector3(25, 0, -60));
        fountainPositions.Add(new Vector3(-25, 0, -60));
        fountainPositions.Add(new Vector3(-25, 0, 60));

        int number = Random.Range(0, 100);
        if(number < 75) 
            return Vector3.zero;

        return fountainPositions[Random.Range(0, fountainPositions.Count - 1)];
    }
}
