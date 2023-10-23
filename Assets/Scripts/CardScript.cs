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
    public CardData cardData;

    private GameObject _building;

    public void Init(CardData data)
    {
        cardData = data;

        _building = Resources.Load<GameObject>("Prefabs/Triangularity/ColorfulCity/Prefabs/Building1_a_MainHall_LP_0");
        _building.name = $"Model_{cardData.Color.ToString()}_{cardData.Value.ToString()}";
    }

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

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
