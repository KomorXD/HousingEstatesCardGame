using UnityEngine;

[System.Serializable]
public class GameHUDManager : MonoBehaviour
{
    public static GameHUDManager Instance { get; private set; }

    private CardsHUDManager cardsHUD;
    private BombsHUDManager bombsHUD;

    public void UpdateUI()
    {
        cardsHUD.UpdateUI();
        bombsHUD.UpdateUI();
    }
    
    private void Awake()
    {
        Instance = this;

        cardsHUD = FindObjectOfType<CardsHUDManager>();
        bombsHUD = FindObjectOfType<BombsHUDManager>();
    }

    public void Init()
    {
        cardsHUD.Init();
        bombsHUD.Init();
    }
}
