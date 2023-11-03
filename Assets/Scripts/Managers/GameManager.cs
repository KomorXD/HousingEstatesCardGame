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

    public int AvailableBombs { get { return availableBombs; } set { availableBombs = value; } }
    public CardData? SelectedCard { get { return selectedCard; } set { selectedCard = value; } }
    public List<CardData> Deck { get { return cardsDeck; } set { cardsDeck = value; } }
    public int CardsLeft => cardsDeck.Count;
    public bool BombsSelected { get { return bombsSelected; } set { bombsSelected = value; } }
    public Difficulty GameDifficulty => difficulty;
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

    //! Sets up the game, draws random card at the end
    void Start()
    {
        // difficulty = GameData.Instance.GameDifficulty;
        difficulty = DifficultiesManager.Instance.Difficulties[1];
        availableBombs = difficulty.BombsCount;
        selectedCard = null;

        cardsDeck = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Queen, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 10),
                new CardParameter(ParameterCategory.Trees, 20),
                new CardParameter(ParameterCategory.GrossSapceIndex, 3),
            }),
            new CardData(CardColor.Diamonds, CardValue.King, new()
            {
                new CardParameter(ParameterCategory.Trees, 2),
                new CardParameter(ParameterCategory.GrossSapceIndex, 31),
            }),
            new CardData(CardColor.Spades, CardValue.Eight, new()
            {
                new CardParameter(ParameterCategory.DwellingsPerHa, 1),
                new CardParameter(ParameterCategory.GreenSpaceIndex, 1),
                new CardParameter(ParameterCategory.Trees, 1),
                new CardParameter(ParameterCategory.GrossSapceIndex, 1),
                new CardParameter(ParameterCategory.AverageFloors, 1),
                new CardParameter(ParameterCategory.FloorRatio, 1),
            }),
            new CardData(CardColor.Clubs, CardValue.Seven, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 1),
            })
        };

        GameHUDManager.Instance.Init();
        gameState = new MainState(this);
    }

    private void Update()
    {
        gameState.Update();
    }

    public void SetState(IGameState state)
    {
        gameState = state;
    }

    /**
     * Returns currently selected card
     * 
     * \returns held card
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

    public CardData? GetPickedCard()
    {
        if(selectedCard == null)
        {
            return null;
        }

        CardData? ret = selectedCard;
        selectedCard = null;

        return ret;
    }

    /**
     * Draws a random card from the deck, if there are any left
     * 
     * \returns a card or null if none is available
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
     * \param size (DEPRACATED) - card's size
     * \param parameters - list of card's parameters and values
     */
    public void ConstructNewCardInDeck(CardColor color, CardValue value, List<CardParameter> parameters)
    {
        cardsDeck.Add(new CardData(color, value, parameters));
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
