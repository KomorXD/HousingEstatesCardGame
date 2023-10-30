using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Button playButton;

    [Header("Inputs")]
    [SerializeField] private TMP_InputField nickInput;

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
        playButton.onClick.AddListener(PlayButtonClick);
    }

    private void BackButtonClick()
    {
        gameInputPanel.GetComponent<MenuPanelAnimator>().Right();
        startMenuPanel.GetComponent<MenuPanelAnimator>().Right();
        nickInput.text = string.Empty;
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

    private void PlayButtonClick()
    {
        if (nickInput.text.Length < 1)
            return;

        PlayerPrefs.SetString("nickname", nickInput.text);
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        Debug.Log(nickInput.text.Length);
        Debug.Log(nickInput.text);
    }
}
