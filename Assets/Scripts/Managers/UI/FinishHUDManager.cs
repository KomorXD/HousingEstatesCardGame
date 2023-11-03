using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishHUDManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button playButton; 
    [SerializeField] private Button quitButton;

    [SerializeField] private RectTransform imageHolder;
    [SerializeField] private RectTransform playBtnHolder;
    [SerializeField] private RectTransform quitBtnHolder;

    [SerializeField] private float slideDuration = 0.7f;

    private void Awake()
    {
        string filename = GameData.Instance.PlayerWon ? "won" : "lost";

        Texture2D tex = Resources.Load<Texture2D>($"Textures/{filename}");
        image.sprite = Sprite.Create(tex, new(0.0f, 0.0f, tex.width, tex.height), Vector2.zero);

        imageHolder.DOLocalMoveY(130.0f, slideDuration).SetEase(Ease.OutSine);
        playBtnHolder.DOLocalMoveX(0.0f, slideDuration).SetEase(Ease.OutSine);
        quitBtnHolder.DOLocalMoveX(0.0f, slideDuration).SetEase(Ease.OutSine);

        Invoke(nameof(EnableButtons), slideDuration);
    }
    
    private void EnableButtons()
    {
        playButton.interactable = true;
        quitButton.interactable = true;

        playButton.onClick.AddListener(OnPlayButtonPressed);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    private void DisableButtons()
    {
        playButton.interactable = false;
        quitButton.interactable = false;

        playButton.onClick.RemoveListener(OnPlayButtonPressed);
        quitButton.onClick.RemoveListener(OnQuitButtonPressed);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    private void OnPlayButtonPressed()
    {
        DisableButtons();

        transform.DOMoveY(-4000.0f, 1.0f).SetEase(Ease.InSine);

        Invoke(nameof(LoadMenuScene), 1.0f);
    }

    private void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
