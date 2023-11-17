using System.Collections.Generic;
using UnityEngine;

//! A struct representing a Card's data
[System.Serializable]
public struct CardData
{
    //! Card's color
    public CardColor Color;

    //! Card's value
    public CardValue Value;

    //! Card's parameters
    public List<CardParameter> Parameters;

    //! Intializes properties with given values
    public CardData(CardColor color, CardValue value, List<CardParameter> parameters)
    {
        Color = color;
        Value = value;
        Parameters = parameters;
    }
}

//! Represents a single parameter
[System.Serializable]
public struct CardParameter
{
    //! Parameter's type
    public ParameterCategory Category;

    //! Parameter's value
    public int Value;

    //! Intializes properties with given values
    public CardParameter(ParameterCategory category, int points)
    {
        Category = category;
        Value = points;
    }
}

//! A class responsible for managing a single card behaviour
public class CardScript : MonoBehaviour
{
    [SerializeField] private CardData cardData;
	
    //! Card's data
    public CardData Data { get { return cardData; } }
    
    private GameObject buildingObject;
    private GameObject fountainObject;
    private GameObject treeObject;
    private List<GameObject> treesObjects = new List<GameObject>();

    //! Intializes data and grabs an apropriate model, based on card's data
    public void Init(CardData data)
    {
        cardData = data;
    }

    //! Spawns card and it's model
    public GameObject PlaceBuilding(Vector3 position)
    {
        GameObject buildingPrefab = Resources.Load<GameObject>("Prefabs/Triangularity/ColorfulCity/Prefabs/Building1_a_MainHall_LP_0");
        
        buildingPrefab.name = $"Model_{cardData.Color}_{cardData.Value}";
        buildingPrefab.transform.position = position;
        buildingPrefab.transform.localScale = 2.7f * 3.0f * Vector3.one;
        
        buildingObject = Instantiate(buildingPrefab, gameObject.transform);
        buildingObject.name = $"Model_{cardData.Color}_{cardData.Value}";
        buildingObject.layer = LayerMask.NameToLayer("CardBuilding");
        Destroy(buildingObject.GetComponent<MeshCollider>()); // for now, since placeholder models have that

        BoxCollider collider = buildingObject.AddComponent<BoxCollider>();
        Mesh mesh = buildingObject.GetComponent<MeshFilter>().mesh;

        collider.center = buildingObject.transform.parent.position + Vector3.up * mesh.bounds.size.y / 2.0f;
        collider.size = mesh.bounds.size;

        return buildingObject;
    }

    //! Spawns fountain
    public GameObject PlaceFountain(Vector3 position)
    {
        Vector3 offset = GetComponent<CardObjectGenerator>().GetFountainPosition();

        if (offset == Vector3.zero)
            return null;

        GameObject fountainPrefab = Resources.Load<GameObject>("Prefabs/Fountain");

        fountainPrefab.name = $"Fountain";
        fountainPrefab.transform.position = position + offset;

        fountainObject = Instantiate(fountainPrefab, gameObject.transform);
        fountainObject.name = fountainPrefab.name;
        fountainObject.layer = LayerMask.NameToLayer("CardBuilding");

        return fountainObject;
    }

    //! Spawns trees
    public List<GameObject> PlaceTrees(Vector3 position)
    {
        List<Vector3> positions = GetComponent< CardObjectGenerator>().GetTreesPosition();

        int i = 0;
        foreach (Vector3 pos in positions)
        {
            GameObject treePrefab = GetComponent<CardObjectGenerator>().GetTree();

            treePrefab.name = $"Tree{i}";
            treePrefab.transform.position = position + pos;

            treeObject = Instantiate(treePrefab, gameObject.transform);
            treeObject.name = treePrefab.name;
            treeObject.layer = LayerMask.NameToLayer("CardBuilding");     
            treesObjects.Add(treeObject);
            i++;
        }
        return treesObjects;
    }

    //! Despawns objects on card
    public void Despawn()
    {
        DestroyImmediate(buildingObject, true);
        DestroyImmediate(fountainObject, true);
        foreach(var tree in treesObjects)
            DestroyImmediate(tree, tree);
    }
}
