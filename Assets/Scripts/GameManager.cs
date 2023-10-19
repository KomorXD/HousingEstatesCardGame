using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private List<CardData> _playerCards;
    [SerializeField] private List<CardData> _availableCards;

    private GameObject _env;
    
    void Start()
    {
        // _env = GameObject.FindGameObjectWithTag("EnvTag");

        _availableCards = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Six, new Vector2(3, 2), new()),
            new CardData(CardColor.Diamonds, CardValue.Seven, new Vector2(2, 2), new()),
            new CardData(CardColor.Spades, CardValue.Eight, new Vector2(2, 1), new()),
            new CardData(CardColor.Clubs, CardValue.Nine, new Vector2(3, 1), new())
        };
    }
    
    void Update()
    {
        
    }

    public void AddNewCardToDeck(CardData newCard)
    {
        _availableCards.Add(newCard);
    }

    public void ConstructNewCardInDeck(CardColor color, CardValue value, Vector2 size, List<CardParameter> parameters)
    {
        _availableCards.Add(new CardData(color, value, size, parameters));
    }

    public CardData GetCardAt(int index)
    {
        return _availableCards[index];
    }

    public void EraseCardAt(int index)
    {
        _availableCards.RemoveAt(index);
    }

    public void UpdateCardAt(int index, CardData card)
    {
        _availableCards[index] = card;
    }
}
