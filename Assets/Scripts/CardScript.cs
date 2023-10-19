using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct CardData
{
    public CardColor Color;
    public CardValue Value;
    public Vector2 Size;
    public List<CardParameter> Parameters;

    public CardData(CardColor color, CardValue value, Vector2 size, List<CardParameter> parameters)
    {
        Color = color;
        Value = value;
        Size = size;
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

    private GameObject _prefab;

    public void Init(CardData data)
    {
        GameObject env = GameObject.FindGameObjectWithTag("EnvTag");

        CardData = data;

        _prefab = Resources.Load<GameObject>("Prefabs/Triangularity/ColorfulCity/Prefabs/Building1_a_MainHall_LP_0");
        _prefab = Instantiate(_prefab, env.transform);
        _prefab.name = $"Model_{CardData.Color.ToString()}_{CardData.Value.ToString()}";
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
