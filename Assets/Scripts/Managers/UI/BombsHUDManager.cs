using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//! Manages bombs button in-game
[System.Serializable]
public class BombsHUDManager : MonoBehaviour, IHUDManager, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float animationDeltaY = 10.0f;
    [SerializeField] private float moveAnimationDuration = 0.25f;
    [SerializeField] private float animationScale = 0.75f;
    [SerializeField] private float scaleAnimationDuration = 0.1f;

    private Button bombsButton;
    private TMP_Text bombsLeftText;

    //! Sets bomb texture, adds on click listener
    public void Init()
    {
        Texture2D bombTex = Resources.Load<Texture2D>("Textures/gragas_bomba");
        bombsButton.GetComponent<Image>().sprite = Sprite.Create(bombTex, new(0.0f, 0.0f, bombTex.width, bombTex.height), Vector2.zero);
        bombsButton.onClick.AddListener(OnBombsSelected);

        UpdateUI();
    }

    //! Updates text
    public void UpdateUI()
    {
        bombsLeftText.text = $"{(GameManager.Instance.BombsSelected ? "Bomba" : "Nej bomba :(")}" + $" {GameManager.Instance.AvailableBombs}";
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
        bombsButton.interactable = interactive;
    }

    private void Awake()
    {
        bombsButton = GetComponentInChildren<Button>();
        bombsLeftText = GetComponentInChildren<TMP_Text>();
    }
    
    private void OnBombsSelected()
    {
        // bombsButton.transform.DOScale(animationScale, scaleAnimationDuration).SetEase(Ease.InOutSine);
        Invoke(nameof(OnResetButtonClickAnimation), scaleAnimationDuration);
        GameManager.Instance.BombsSelected = !GameManager.Instance.BombsSelected;
        UpdateUI();
    }

    private void OnResetButtonClickAnimation()
    {
        // bombsButton.transform.DOScale(bombsButton.transform.localScale.x, scaleAnimationDuration).SetEase(Ease.InOutSine);
    }

    //! Moves bomb slightly on pointer enter, event handler
    public void OnPointerEnter(PointerEventData eventData)
    {
        bombsButton.transform.DOMoveY(transform.position.y - animationDeltaY, moveAnimationDuration).SetEase(Ease.InOutSine);
    }

    //! Moves bomb back on pointer end, event handler
    public void OnPointerExit(PointerEventData eventData)
    {
        bombsButton.transform.DOMoveY(transform.position.y, moveAnimationDuration).SetEase(Ease.InOutSine);
    }
}
