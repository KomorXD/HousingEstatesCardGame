using System.Collections.Generic;
using UnityEngine;

//! Class responsible for managing the game itself, a singleton
public class GameManager : MonoBehaviour
{
    //! Class' instance
    public static GameManager Instance { get; private set; }

    [SerializeField] private IGameState gameState;
    private Difficulty difficulty;
    private Dictionary<ParameterCategory, float> gameParameters;

    [SerializeField] private int availableBombs;
    [SerializeField] private CardData? selectedCard;
    [SerializeField] private List<CardData> cardsDeck;
    [SerializeField] private bool bombsSelected;

    //! Available bombs count
    public int AvailableBombs { get { return availableBombs; } set { availableBombs = value; } }

    //! Selected card's data
    public CardData? SelectedCard { get { return selectedCard; } set { selectedCard = value; } }

    //! Card available in a deck
    public List<CardData> Deck { get { return cardsDeck; } set { cardsDeck = value; } }

    //! How many cards are left in a deck
    public int CardsLeft => cardsDeck.Count;

    //! Whether bombs are selected
    public bool BombsSelected { get { return bombsSelected; } set { bombsSelected = value; } }

    //! Set difficulty
    public Difficulty GameDifficulty => difficulty;

    //! Parameter points
    public Dictionary<ParameterCategory, float> GameParameters => gameParameters;

    private void Awake()
    {
        Instance = this;
        gameParameters = new()
        {
            { ParameterCategory.DwellingsPerHa, 0.0f },
            { ParameterCategory.Trees, 0.0f },
            { ParameterCategory.GreenSpaceIndex, 0.0f },
            { ParameterCategory.GrossSapceIndex, 0.0f },
            { ParameterCategory.FloorRatio, 0.0f },
            { ParameterCategory.AverageFloors, 0.0f }
        };
    }

    //! Sets up the game
    void Start()
    {
        difficulty = GameData.Instance.GameDifficulty;
        availableBombs = difficulty.BombsCount;
        selectedCard = null;

        cardsDeck = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Queen, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 10),
                new CardParameter(ParameterCategory.Trees, 20),
                new CardParameter(ParameterCategory.GrossSapceIndex, 3),
            }, ParameterCategory.FloorRatio),
            new CardData(CardColor.Diamonds, CardValue.King, new()
            {
                new CardParameter(ParameterCategory.Trees, 2),
                new CardParameter(ParameterCategory.GrossSapceIndex, 31),
            }, ParameterCategory.DwellingsPerHa),
            new CardData(CardColor.Spades, CardValue.Eight, new()
            {
                new CardParameter(ParameterCategory.DwellingsPerHa, 1),
                new CardParameter(ParameterCategory.GreenSpaceIndex, 1),
                new CardParameter(ParameterCategory.Trees, 1),
                new CardParameter(ParameterCategory.GrossSapceIndex, 1),
                new CardParameter(ParameterCategory.AverageFloors, 1),
                new CardParameter(ParameterCategory.FloorRatio, 1),
            }, ParameterCategory.Trees),
            new CardData(CardColor.Clubs, CardValue.Seven, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 1),
            }, ParameterCategory.Trees)
        };

        GameHUDManager.Instance.Init();
        gameState = new MainState(this);
    }

    private void Update()
    {
        gameState.Update();
    }

    /**
     * Changes game state
     * 
     * \param state New state
     */
    public void SetState(IGameState state)
    {
        gameState = state;
    }

    /**
     * Returns currently selected card
     * 
     * \returns Currently held card
     */
    public GameObject GetPlayerCard()
    {
        if(selectedCard == null)
        {
            return null;
        }

        GameObject cardObject = Resources.Load<GameObject>("Prefabs/Card");
        cardObject.GetComponent<CardScript>().Init((CardData)selectedCard);
        selectedCard = null;

        GameHUDManager.Instance.UpdateUI();

        return cardObject;
    }

    /**
     * Draws a random card from the deck, if there are any left
     */
    public void DrawRandomCard()
    {
        int randomIdx = Random.Range(0, cardsDeck.Count);

        if(randomIdx < 0 || randomIdx >= cardsDeck.Count)
        {
            return;
        }

        CardData card = cardsDeck[randomIdx];

        cardsDeck.RemoveAt(randomIdx);
        selectedCard = card;
    }

    /**
     * Adds a new card to the deck
     * 
     * \param newCard - new card to add
     */
    public void AddNewCardToDeck(CardData newCard)
    {
        cardsDeck.Add(newCard);
    }

    /**
     * Construct a new card in place
     * 
     * \param color - card's color
     * \param value - card's value
     * \param parameters - list of card's parameters and values
     */
    public void ConstructNewCardInDeck(CardColor color, CardValue value, List<CardParameter> parameters, ParameterCategory pref)
    {
        cardsDeck.Add(new CardData(color, value, parameters, pref));
    }

    /**
     * Returns card at the given index
     * 
     * \param index - index
     * \returns card
     */
    public CardData GetCardAt(int index)
    {
        return cardsDeck[index];
    }

    /**
     * Erases card at the given index
     * 
     * \param index - index
     */
    public void EraseCardAt(int index)
    {
        cardsDeck.RemoveAt(index);
    }

    /**
     * Updates card at the given index
     * 
     * \param index - index
     * \param card - updated card
     */
    public void UpdateCardAt(int index, CardData card)
    {
        cardsDeck[index] = card;
    }
}
