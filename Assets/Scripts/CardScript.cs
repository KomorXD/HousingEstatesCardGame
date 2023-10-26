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
    public CardData cardData;

    private GameObject _building;

    //! Intializes data and grabs an apropriate model, based on card's data
    public void Init(CardData data)
    {
        cardData = data;

        _building = Resources.Load<GameObject>("Prefabs/Triangularity/ColorfulCity/Prefabs/Building1_a_MainHall_LP_0");
        _building.name = $"Model_{cardData.Color.ToString()}_{cardData.Value.ToString()}";
    }

    //! Spawns card and it's model
    public void Spawn(GameObject card, Vector3 position)
    {
        GameObject env = GameObject.FindGameObjectWithTag("BoardTag");

        position.y += 0.01f;
        card.transform.position = position;
        _building.transform.position = position;
        _building.transform.localScale = 0.1f * Vector3.one;

        GameObject newCard = Instantiate(card, env.transform);
        Texture2D texture2D = Resources.Load<Texture2D>($"Textures/Cards/{cardData.Value.ToString()}{cardData.Color.ToString()}");
        MeshRenderer meshRenderer = newCard.GetComponent<MeshRenderer>();

        if (meshRenderer != null && texture2D)
        {
            meshRenderer.material.mainTexture = texture2D;
        }
        
        Instantiate(_building, env.transform);
    }
}
