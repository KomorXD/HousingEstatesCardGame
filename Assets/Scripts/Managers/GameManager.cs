using System.Collections.Generic;
using UnityEngine;

//! Class responsible for managing the game itself, a singleton
public class GameManager : MonoBehaviour
{
    //! Class' instance
    public static GameManager Instance { get; private set; }

    [SerializeField] private IGameState gameState;

    [SerializeField] private int availableBombs;
    [SerializeField] private CardData? selectedCard;
    [SerializeField] private List<CardData> cardsDeck;
    [SerializeField] private bool bombsSelected;

    public int AvailableBombs { get { return availableBombs; } set { availableBombs = value; } }
    public CardData? SelectedCard { get { return selectedCard; } set { selectedCard = value; } }
    public List<CardData> Deck { get { return cardsDeck; } set { cardsDeck = value; } }
    public int CardsLeft => cardsDeck.Count;
    public bool BombsSelected { get { return bombsSelected; } set { bombsSelected = value; } }

    public void SetState(IGameState state)
    {
        gameState = state;
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
    public void ConstructNewCardInDeck(CardColor color, CardValue value, Vector2 size, List<CardParameter> parameters)
    {
        cardsDeck.Add(new CardData(color, value, size, parameters));
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

    private void Awake()
    {
        Instance = this;
        GameHUDManager.Instance.Init();
    }

    //! Sets up the game, draws random card at the end
    void Start()
    {
        gameState = new MainState(this);
    }

    private void Update()
    {
        gameState.Update();
    }
}
