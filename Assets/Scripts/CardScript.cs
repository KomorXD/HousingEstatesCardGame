using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct CardData
{
    public CardColor Color;
    public CardValue Value;
    public List<CardParameter> Parameters;

    public CardData(CardColor color, CardValue value, List<CardParameter> parameters)
    {
        Color = color;
        Value = value;
        Parameters = parameters;
    }
}

[System.Serializable]
public struct CardParameter
{
    public ParameterCategory Category;
    public int Points;

    public CardParameter(ParameterCategory category, int points)
    {
        Category = category;
        Points = points;
    }
}

public class CardScript : MonoBehaviour
{
    public CardData CardData;

    public void Init(CardData data)
    {
        CardData = data;

        // Tesktura, modele
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
