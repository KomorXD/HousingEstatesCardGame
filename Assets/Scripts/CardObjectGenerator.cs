using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CardObjectGenerator : MonoBehaviour
{
    private List<Vector3> fountainPositions = new List<Vector3>();
    private List<Vector3> treesPositions = new List<Vector3>();
    public List<GameObject> trees = new List<GameObject>();

    /**
     * Returns position of fountain to spawn
     * 
     * \returns Position of fountain
     */
    public Vector3 GetFountainPosition()
    {
        fountainPositions.Add(new Vector3(25, 0, 60));
        fountainPositions.Add(new Vector3(25, 0, -60));
        fountainPositions.Add(new Vector3(-25, 0, -60));
        fountainPositions.Add(new Vector3(-25, 0, 60));

        int number = Random.Range(0, 100);
        if(number < 50) 
            return Vector3.zero;

        return fountainPositions[Random.Range(0, fountainPositions.Count - 1)];
    }

    /**
     * Returns random tree prefab
     * 
     * \returns Random tree prefab
     */
    public GameObject GetTree()
    {
        return trees[Random.Range(0, trees.Count - 1)];
    }

    /**
     * Returns positions of tree to spawn
     * 
     * \returns Positions of tree to spawn
     */
    public List<Vector3> GetTreesPosition()
    {
        treesPositions.Add(new Vector3(-33, 0, -70));
        treesPositions.Add(new Vector3(-32, 0, -65));
        treesPositions.Add(new Vector3(-31, 0, -50));
        treesPositions.Add(new Vector3(-32, 0, -40));
        treesPositions.Add(new Vector3(-30, 0, -45));

        treesPositions.Add(new Vector3(33, 0, -70));
        treesPositions.Add(new Vector3(32, 0, -65));
        treesPositions.Add(new Vector3(31, 0, -50));
        treesPositions.Add(new Vector3(32, 0, -40));
        treesPositions.Add(new Vector3(30, 0, -45));

        treesPositions.Add(new Vector3(-33, 0, 70));
        treesPositions.Add(new Vector3(-32, 0, 65));
        treesPositions.Add(new Vector3(-31, 0, 50));
        treesPositions.Add(new Vector3(-32, 0, 40));
        treesPositions.Add(new Vector3(-30, 0, 45));

        treesPositions.Add(new Vector3(33, 0, 70));
        treesPositions.Add(new Vector3(32, 0, 65));
        treesPositions.Add(new Vector3(31, 0, 50));
        treesPositions.Add(new Vector3(32, 0, 40));
        treesPositions.Add(new Vector3(30, 0, 45));

        treesPositions.Add(new Vector3(-20, 0, 60));
        treesPositions.Add(new Vector3(-15, 0, 65));
        treesPositions.Add(new Vector3(-10, 0, 70));
        treesPositions.Add(new Vector3(0,   0, 62));
        treesPositions.Add(new Vector3(10,  0, 58));
        treesPositions.Add(new Vector3(15,  0, 65));
        treesPositions.Add(new Vector3(20,  0, 55));

        treesPositions.Add(new Vector3(-20, 0, -60));
        treesPositions.Add(new Vector3(-15, 0, -65));
        treesPositions.Add(new Vector3(-10, 0, -70));
        treesPositions.Add(new Vector3(0,   0, -62));
        treesPositions.Add(new Vector3(10,  0, -58));
        treesPositions.Add(new Vector3(15,  0, -65));
        treesPositions.Add(new Vector3(20,  0, -55));

        List<Vector3> treesToGenerate = new List<Vector3>();
        treesToGenerate.AddRange(treesPositions);

        foreach (var tree in treesPositions)
        {
            int number = Random.Range(0, 100);
            if (number < 66)
            {
                treesToGenerate.Remove(tree);
            }
        }
        return treesToGenerate;
    }
}
