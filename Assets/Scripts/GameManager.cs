using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<CardData> _playerCards;
    [SerializeField] private CardData _playerCard;
    [SerializeField] private List<CardData> _availableCards;

    private GameObject _env;
    private bool noCards = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _env = GameObject.FindGameObjectWithTag("EnvTag");

        _availableCards = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Queen, new Vector2(3, 2), new()),
            new CardData(CardColor.Diamonds, CardValue.King, new Vector2(2, 2), new()),
            new CardData(CardColor.Spades, CardValue.Eight, new Vector2(2, 1), new()),
            new CardData(CardColor.Clubs, CardValue.Seven, new Vector2(3, 1), new())
        };

        DrawRandomCard();
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

    public GameObject GetPlayerCard()
    {
        if(noCards)
            return null;

        if (_availableCards.Count == 0 && noCards == false)
            noCards = true; 

        GameObject cardToPlay = Resources.Load<GameObject>("Prefabs/Card");
        cardToPlay.name = $"Card_{_playerCard.Color.ToString()}_{_playerCard.Value.ToString()}";
        cardToPlay.GetComponent<CardScript>().Init(_playerCard);
        return cardToPlay;
    }

    public CardData? DrawRandomCard()
    {
        if (_availableCards.Count == 0)
            return null;

        int xd = Random.Range(0, _availableCards.Count);
        CardData card = _availableCards[xd];
        _availableCards.RemoveAt(xd);
        _playerCard = card;
        return card;
    }
}
