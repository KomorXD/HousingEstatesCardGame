using UnityEngine;
using UnityEngine.UI;

public class WalkEstateHUDManager : MonoBehaviour, IHUDManager
{
    private Button walkButton;

    public void Init()
    {
        walkButton.onClick.AddListener(StartTheWalk);
    }

    public void UpdateUI()
    {
        ;
    }

    private void Awake()
    {
        walkButton = GetComponentInChildren<Button>();
    }

    private void StartTheWalk()
    {
        GameManager.Instance.SetState(new WalkAroundEstateState(GameManager.Instance));
    }
}
