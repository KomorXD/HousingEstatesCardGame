using System.Collections.Generic;
using System.Linq;
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
            FindObjectOfType<WalkEstateHUDManager>()
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

    private void Awake()
    {
        Instance = this;
    }

    public void SetInteractive(bool interactive)
    {
        foreach (var manager in hudManagers)
        {
            manager.SetInteractive(interactive);
        }
    }
}
