using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameHUDManager : MonoBehaviour, IHUDManager
{
    public static GameHUDManager Instance { get; private set; }

    private List<IHUDManager> hudManagers;

    public void Init()
    {
        hudManagers = new()
        {
            FindObjectOfType<CardsHUDManager>(),
            FindObjectOfType<BombsHUDManager>(),
            FindObjectOfType<ControlButtonsHUDManager>(),
            FindObjectOfType<GameInfoHUDManager>(),
            FindObjectOfType<PointsHUDManager>()
        };

        foreach (var manager in hudManagers)
        {
            manager.Init();
        }
    }

    public void UpdateUI()
    {
        foreach (var manager in hudManagers)
        {
            manager.UpdateUI();
        }
    }

    public void SetBombsInteractive(bool interactive)
    {
        FindObjectOfType<BombsHUDManager>().SetInteractive(interactive);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetActive(bool active)
    {
        foreach (var manager in hudManagers)
        {
            manager.SetActive(active);
        }
    }

    public void SetInteractive(bool interactive)
    {
        foreach (var manager in hudManagers)
        {
            manager.SetInteractive(interactive);
        }
    }
}
