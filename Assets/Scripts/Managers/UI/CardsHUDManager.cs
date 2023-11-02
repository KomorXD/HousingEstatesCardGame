using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardsHUDManager : MonoBehaviour, IHUDManager
{
    private Image selectedCardImage;
    private Button nextCardButton;
    private TMP_Text nextCardButtonText;

    public void Init()
    {
        nextCardButton.onClick.AddListener(OnNextCard);
        UpdateUI();
    }

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

        selectedCardImage.sprite = Sprite.Create(cardTex, new(0.0f, 0.0f, cardTex.width, cardTex.height), Vector2.zero);
        nextCardButtonText.text = $"Next card ({GameManager.Instance.CardsLeft} left)";
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

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
        GameManager.Instance.DrawRandomCard();
        UpdateUI();
    }
}
