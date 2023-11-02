using UnityEngine;
using UnityEngine.UI;

public class ControlButtonsHUDManager : MonoBehaviour, IHUDManager
{
    [SerializeField] private Button walkButton;
    [SerializeField] private Button finishButton;

    public void Init()
    {
        walkButton.onClick.AddListener(StartTheWalk);
        finishButton.onClick.AddListener(FinishGame);
    }

    public void UpdateUI()
    {
        ;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetInteractive(bool interactive)
    {
        walkButton.interactable = interactive;
        finishButton.interactable = interactive;
    }

    private void StartTheWalk()
    {
        GameManager.Instance.SetState(new WalkAroundEstateState(GameManager.Instance));
    }

    private void FinishGame()
    {
        GameManager.Instance.SetState(new GameFinishedState());
    }
}
