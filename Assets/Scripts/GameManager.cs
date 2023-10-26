using System.Collections.Generic;
using UnityEngine;

//! Class responsible for managing the game itself, a singleton
public class GameManager : MonoBehaviour
{
    //! Class' instance
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

    //! Sets up the game, draws random card at the end
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

    /**
     * Adds a new card to the deck
     * 
     * \param newCard - new card to add
     */
    public void AddNewCardToDeck(CardData newCard)
    {
        _availableCards.Add(newCard);
    }

    /**
     * Construct a new card in place
     * 
     * \param color - card's color
     * \param value - card's value
     * \param size (DEPRACATED) - card's size
     * \param parameters - list of card's parameters and values
     */
    public void ConstructNewCardInDeck(CardColor color, CardValue value, Vector2 size, List<CardParameter> parameters)
    {
        _availableCards.Add(new CardData(color, value, size, parameters));
    }

    /**
     * Returns card at the given index
     * 
     * \param index - index
     * \returns card
     */
    public CardData GetCardAt(int index)
    {
        return _availableCards[index];
    }

    /**
     * Erases card at the given index
     * 
     * \param index - index
     */
    public void EraseCardAt(int index)
    {
        _availableCards.RemoveAt(index);
    }
    /**
     * Updates card at the given index
     * 
     * \param index - index
     * \param card - updated card
     */
    public void UpdateCardAt(int index, CardData card)
    {
        _availableCards[index] = card;
    }

    /**
     * Returns currently selected card
     * 
     * \returns held card
     */
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

    /**
     * Draws a random card from the deck, if there are any left
     * 
     * \returns a card or null if none is available
     */
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
