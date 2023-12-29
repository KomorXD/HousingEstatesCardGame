using System.Collections.Generic;
using UnityEngine;

//! Class responsible for calling whole game UI
[System.Serializable]
public class GameHUDManager : MonoBehaviour, IHUDManager
{
	//! Singleton instance
    public static GameHUDManager Instance { get; private set; }

    private List<IHUDManager> hudManagers;

    //! Initializes each UI elements
    public void Init()
    {
        hudManagers = new()
        {
            FindObjectOfType<CardsHUDManager>(),
            FindObjectOfType<BombsHUDManager>(),
            FindObjectOfType<ControlButtonsHUDManager>(),
            FindObjectOfType<GameInfoHUDManager>(),
            FindObjectOfType<PointsHUDManager>(),
            FindObjectOfType<CardHandScript>()
        };

        foreach (var manager in hudManagers)
        {
            manager.Init();
        }
    }

    //! Updates each UI elements
    public void UpdateUI()
    {
        foreach (var manager in hudManagers)
        {
            manager.UpdateUI();
        }
    }

    //! Sets bombs' UI interactivity
    public void SetBombsInteractive(bool interactive)
    {
        FindObjectOfType<BombsHUDManager>().SetInteractive(interactive);
    }

    private void Awake()
    {
        Instance = this;
    }

    //! Sets each UI elements active flag
    public void SetActive(bool active)
    {
        foreach (var manager in hudManagers)
        {
            manager.SetActive(active);
        }
    }

    //! Sets each UI elements interactive flag
    public void SetInteractive(bool interactive)
    {
        foreach (var manager in hudManagers)
        {
            manager.SetInteractive(interactive);
        }
    }
}
