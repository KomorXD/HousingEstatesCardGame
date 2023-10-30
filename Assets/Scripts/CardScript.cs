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

    //! depracated
    public Vector2 Size;

    //! Card's parameters
    public List<CardParameter> Parameters;

    //! Intializes properties with given values
    public CardData(CardColor color, CardValue value, Vector2 size, List<CardParameter> parameters)
    {
        Color = color;
        Value = value;
        Size = size;
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
    public int Points;

    //! Intializes properties with given values
    public CardParameter(ParameterCategory category, int points)
    {
        Category = category;
        Points = points;
    }
}

//! A class responsible for managing a single card behaviour
public class CardScript : MonoBehaviour
{
    //! Card's data
    private CardData cardData;
    
    private GameObject buildingObject;

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
        buildingPrefab.transform.localScale = 0.1f * Vector3.one;
        
        buildingObject = Instantiate(buildingPrefab);
        buildingObject.name = $"Model_{cardData.Color}_{cardData.Value}";
        buildingObject.layer = LayerMask.NameToLayer("CardBuilding");

        return buildingObject;
    }
    
    public void Despawn()
    {
        DestroyImmediate(buildingObject, true);
    }
}
