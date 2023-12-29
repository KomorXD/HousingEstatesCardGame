using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardHandScript : MonoBehaviour
{
    public Vector2 position;
    public float handWidth = 200;
    private float cardWidth;

    private CardData selectedCard;
    private List<CardData> cardsData;
    private List<CardImage> cards = new List<CardImage>();

    // Start is called before the first frame update
    void Start()
    {
        CreateHand();
        PlaceCards();
    }

    public void DrawCard()
    {
        if (cardsData.Count >= 5)
            return;
        AddCard(new CardData(CardColor.Hearts, CardValue.Queen, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 10),
                new CardParameter(ParameterCategory.Trees, 20),
                new CardParameter(ParameterCategory.GrossSapceIndex, 3),
            }, ParameterCategory.FloorRatio));
    }

    public void SelectCard()
    {
        RemoveCard(new CardData(CardColor.Hearts, CardValue.Queen, new()
            {
                new CardParameter(ParameterCategory.GreenSpaceIndex, 10),
                new CardParameter(ParameterCategory.Trees, 20),
                new CardParameter(ParameterCategory.GrossSapceIndex, 3),
            }, ParameterCategory.FloorRatio));
    }

    public void AddCard(CardData card)
    {
        foreach (CardImage cardImage in cards)
        {
            Destroy(cardImage.card.gameObject);
        }
        cards.Clear();
        cardsData.Add(card);
        SetImages();
        PlaceCards();
    }

    public void RemoveCard(CardData card)
    {
        foreach (CardImage cardImage in cards)
        {
            Destroy(cardImage.card.gameObject);
        }
        cards.Clear();
        cardRemove(card);
        SetImages();
        PlaceCards();
    }

    void PlaceCards()
    {
        int cardsCount = cards.Count;

        float cardSpace = (handWidth - 2 * cardWidth) / (cardsCount - 1);
        for (int i = 0; i < cardsCount; i++)
        {
            Vector3 cardPosition = new Vector3(position.x + cardWidth + i * cardSpace, position.y, 0);
            cards[i].card.transform.position = cardPosition;
        }
    }

    void cardRemove(CardData card)
    {
        foreach (CardData cardData in cardsData)
        {
            if (cardData.Color == card.Color && cardData.Value == card.Value)
            {
                cardsData.Remove(cardData);
                return;
            }
        }
    }

    void CreateHand()
    {
        cardsData = new List<CardData>
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
        SetImages();
    }

    void SetImages()
    {
        foreach(CardData card in cardsData)
        {
            cards.Add(new CardImage(card, this.gameObject));
        }
    }
}
