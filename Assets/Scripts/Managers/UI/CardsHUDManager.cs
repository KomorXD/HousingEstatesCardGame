using TMPro;
using UnityEngine;
using UnityEngine.UI;

//! Class responsible for managing card UI segment
[System.Serializable]
public class CardsHUDManager : MonoBehaviour, IHUDManager
{
    private Image selectedCardImage;
    private Button nextCardButton;
    private TMP_Text nextCardButtonText;

    //! Adds on click listener
    public void Init()
    {
        nextCardButton.onClick.AddListener(OnNextCard);
        UpdateUI();
    }

    //! Updates UI
    public void UpdateUI()
    {
        if (GameManager.Instance.SelectedCard == null)
        {
            selectedCardImage.enabled = false;

            return;
        }

        if (GameManager.Instance.CardsLeft == 0)
        {
            nextCardButton.interactable = false;
        }

        selectedCardImage.enabled = true;

        CardData card = GameManager.Instance.SelectedCard.Value;
        Texture2D cardTex = Resources.Load<Texture2D>($"Textures/Cards/{card.Value}{card.Color}");
        //if( cardTex != null )
        //{
        //    Debug.Log("karta");
        //}
        //if ( cardTex == null )
        //{
        //    Debug.Log("tekstura");
        //}
        //if(selectedCardImage == null)
        //{
        //    Debug.Log("karta");
        //}
        selectedCardImage.sprite = Sprite.Create(cardTex, new(0.0f, 0.0f, cardTex.width, cardTex.height), Vector2.zero);
        nextCardButtonText.text = $"Next card ({GameManager.Instance.CardsLeft} left)";
    }

    /** Sets component's activity
     * 
     * \param active flag
     */
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /** Sets component's interactivity
     * 
     * \param interactive flag
     */
    public void SetInteractive(bool interactive)
    {
        nextCardButton.interactable = interactive;
    }
    private void Awake()
    {
        selectedCardImage = transform.GetChild(1).GetComponent<Image>();
        nextCardButton = GetComponentInChildren<Button>();
        nextCardButtonText = nextCardButton.GetComponentInChildren<TMP_Text>();
    }

    private void OnNextCard()
    {
        if (CardHandScript.Instance.cards.Count >= 5)
        {
            return;
        }

        GameManager.Instance.BombsSelected = false;     
        CardData? nextCard = GameManager.Instance.DrawRandomCard();
        if(nextCard != null)
        {
            CardHandScript.Instance.AddCard(nextCard.Value);
            UpdateUI();
        }
    }
}
