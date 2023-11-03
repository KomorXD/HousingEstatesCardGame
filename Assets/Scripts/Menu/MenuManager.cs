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
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameInput;
    [SerializeField] private GameObject difficultyMenu;

    [Header("Butons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backInputButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button diffButton;
    [SerializeField] private Button backDiffButton;

    [Header("Inputs")]
    [SerializeField] private TMP_InputField nickInput;

    private void Start()
    {
        AssignUIButtons();
        startMenu.GetComponent<UIAnimator>().Left();
    }

    private void AssignUIButtons()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        backInputButton.onClick.AddListener(BackInputButtonClick);
        playButton.onClick.AddListener(PlayButtonClick);
        diffButton.onClick.AddListener(DiffButtonClick);
        backDiffButton.onClick.AddListener(BackDiffButtonClick);
    }

    private void BackInputButtonClick()
    {
        gameInput.GetComponent<UIAnimator>().Right();
        startMenu.GetComponent<UIAnimator>().Right();
        nickInput.text = string.Empty;
    }

    private void OnStartButtonClick()
    {
        startMenu.GetComponent<UIAnimator>().Left();
        gameInput.GetComponent<UIAnimator>().Left();
    }

    private void OnQuitButtonClick()
    {
        gameInput.SetActive(false);
        Application.Quit();
        Debug.Log("app closed");
    }

    private void PlayButtonClick()
    {
        if (nickInput.text.Length < 1)
            return;

        GameData.Instance.Username = nickInput.text;
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);

        Debug.Log(nickInput.text.Length);
        Debug.Log(nickInput.text);
    }

    private void DiffButtonClick()
    {
        gameInput.GetComponent<UIAnimator>().Left();
        difficultyMenu.GetComponent<UIAnimator>().Left();
    }

    private void BackDiffButtonClick()
    {
        difficultyMenu.GetComponent<UIAnimator>().Right();
        gameInput.GetComponent<UIAnimator>().Right();
    }
}
