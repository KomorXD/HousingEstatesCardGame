using System.Collections.Generic;
using UnityEngine;

//! Class responsible for managing the game itself, a singleton
public class GameManager : MonoBehaviour
{
    //! Class' instance
    public static GameManager Instance { get; private set; }

    [SerializeField] private int availableBombs;
    [SerializeField] private CardData? selectedCard;
    [SerializeField] private List<CardData> cardsDeck;
    [SerializeField] private bool bombsSelected;

    public int AvailableBombs => availableBombs;
    public CardData? SelectedCard => selectedCard;
    public int CardsLeft => cardsDeck.Count;
    public bool BombsSelected { get { return bombsSelected; } set { bombsSelected = value; } }

    private void Awake()
    {
        Instance = this;
    }

    //! Sets up the game, draws random card at the end
    void Start()
    {
        availableBombs = 5;
        selectedCard = null;

        cardsDeck = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Queen, new Vector2(3, 2), new()),
            new CardData(CardColor.Diamonds, CardValue.King, new Vector2(2, 2), new()),
            new CardData(CardColor.Spades, CardValue.Eight, new Vector2(2, 1), new()),
            new CardData(CardColor.Clubs, CardValue.Seven, new Vector2(3, 1), new())
        };

        DrawRandomCard();
        GameHUDManager.Instance.Init();
    }

    private void Update()
    {
        if(!bombsSelected)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitDestructable = Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("CardBuilding");

        if (hitDestructable && Input.GetMouseButtonDown(0))
        {
            // destroy the building
        }
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
        selectedCard = null;
        
        GameObject cardObject = Resources.Load<GameObject>("Prefabs/Card");
        GameHUDManager.Instance.UpdateUI();

        return cardObject;
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
}
