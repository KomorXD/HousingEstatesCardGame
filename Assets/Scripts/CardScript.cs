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

    //! Card's prefered noighbouring parameter
    public ParameterCategory PreferedNeighbour;

    //! Intializes properties with given values
    public CardData(CardColor color, CardValue value, List<CardParameter> parameters, ParameterCategory prefered)
    {
        Color = color;
        Value = value;
        Parameters = parameters;
        PreferedNeighbour = prefered;
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
    private GameObject secretObject;

    //! Intializes data and grabs an apropriate model, based on card's data
    public void Init(CardData data)
    {
        cardData = data;
    }

    //! Spawns card and it's model
    public GameObject PlaceBuilding(Vector3 position, Quaternion rotation)
    {
        GameObject buildingPrefab = Resources.Load<GameObject>("Prefabs/Triangularity/ColorfulCity/Prefabs/Building1_a_MainHall_LP_0");
        
        buildingObject = Instantiate(buildingPrefab, gameObject.transform);
        buildingObject.transform.position = position;
        buildingObject.transform.localScale = 2.7f * 3.0f * Vector3.one;
        buildingObject.transform.localRotation = rotation;
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
    public GameObject PlaceFountain(Vector3 position, Quaternion rotation)
    {
        Vector3 offset = GetComponent<CardObjectGenerator>().GetFountainPosition();

        if (offset == Vector3.zero)
            return null;

        GameObject fountainPrefab = Resources.Load<GameObject>("Prefabs/Fountain");

        fountainObject = Instantiate(fountainPrefab, gameObject.transform);
        fountainObject.name = $"Fountain";
        fountainObject.layer = LayerMask.NameToLayer("CardBuilding");
        fountainObject.transform.position = position + offset;
        fountainObject.transform.Rotate(Vector3.up, rotation.eulerAngles.y);

        return fountainObject;
    }

    //! Spawns trees
    public List<GameObject> PlaceTrees(Vector3 position, Quaternion rotation)
    {
        List<Vector3> positions = GetComponent< CardObjectGenerator>().GetTreesPosition();

        int i = 0;
        foreach (Vector3 pos in positions)
        {
            GameObject treePrefab = GetComponent<CardObjectGenerator>().GetTree();

            treeObject = Instantiate(treePrefab, gameObject.transform);
            treeObject.name = $"Tree{i}";
            treeObject.layer = LayerMask.NameToLayer("CardBuilding");
            treeObject.transform.position = position + pos;
            treeObject.transform.Rotate(Vector3.up, rotation.eulerAngles.y);
            treesObjects.Add(treeObject);
            i++;
        }
        return treesObjects;
    }

    //!Has a chance to spawn secret
    public GameObject SpawnSecret(Vector3 position)
    {
        //if (Random.RandomRange(0, 1) > 0) 
        //    return null;

        GameObject fountainPrefab = Resources.Load<GameObject>("Prefabs/Secret");

        secretObject = Instantiate(fountainPrefab, gameObject.transform);
        secretObject.name = $"Secret";
        secretObject.layer = LayerMask.NameToLayer("CardBuilding");
        secretObject.transform.position = position + new Vector3(Random.RandomRange(-100, 100), 1.0f, Random.RandomRange(-100, 100));

        return secretObject;
    }

    //! Despawns objects on card
    public void Despawn()
    {
        DestroyImmediate(buildingObject, true);
        DestroyImmediate(fountainObject, true);
        foreach(var tree in treesObjects)
            DestroyImmediate(tree, true);
        DestroyImmediate(secretObject, true);
    }
}
