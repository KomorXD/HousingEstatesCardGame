using UnityEngine;
using UnityEngine.UI;

public class ControlButtonsHUDManager : MonoBehaviour, IHUDManager
{
    [SerializeField] private Button walkButton;
    [SerializeField] private Button finishButton;

    //! Adds on click listeners
    public void Init()
    {
        walkButton.onClick.AddListener(StartTheWalk);
        finishButton.onClick.AddListener(FinishGame);
    }

    //! In this case, does nothing
    public void UpdateUI()
    {
        ;
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
