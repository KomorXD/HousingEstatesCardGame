using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject gameInputPanel;

    [Header("Butons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Inputs")]
    [SerializeField] private InputTextScript nickInput;

    private void Awake()
    {
        startMenuPanel.GetComponent<MenuPanelAnimator>().Left();
    }

    private void Start()
    {
        AssignUIButtons();
    }

    private void AssignUIButtons()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        backButton.onClick.AddListener(BackButtonClick);
    }

    private void BackButtonClick()
    {
        gameInputPanel.GetComponent<MenuPanelAnimator>().Right();
        startMenuPanel.GetComponent<MenuPanelAnimator>().Right();
        nickInput.ClearText();
    }

    private void OnStartButtonClick()
    {
        startMenuPanel.GetComponent<MenuPanelAnimator>().Left();
        gameInputPanel.GetComponent<MenuPanelAnimator>().Left();
    }

    private void OnQuitButtonClick()
    {
        gameInputPanel.SetActive(false);
        Application.Quit();
        Debug.Log("app closed");
    }
}
