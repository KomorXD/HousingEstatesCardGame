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
    //! Reference to the start menu GameObject
    [SerializeField] private GameObject startMenu;
    //! Reference to the game input GameObject
    [SerializeField] private GameObject gameInput;
    //! Reference to the difficulty menu GameObject
    [SerializeField] private GameObject difficultyMenu; 

    [Header("Butons")]
    // Reference to the start button
    [SerializeField] private Button startButton;
    // Reference to the quit button
    [SerializeField] private Button quitButton;
    // Reference to the back input button
    [SerializeField] private Button backInputButton;
    // Reference to the play button
    [SerializeField] private Button playButton;

    [Header("Inputs")]
    // Reference to the nickname input field
    [SerializeField] private TMP_InputField nickInput; 

    private void Start()
    {
        AssignUIButtons();
        startMenu.GetComponent<UIAnimator>().Left(); 
    }

    //! Assign click event listeners to the UI buttons
    private void AssignUIButtons()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        backInputButton.onClick.AddListener(BackInputButtonClick);
        playButton.onClick.AddListener(PlayButtonClick);
    }

    //! Handle the action when the back input button is clicked
    private void BackInputButtonClick()
    {
        gameInput.GetComponent<UIAnimator>().Right(); 
        startMenu.GetComponent<UIAnimator>().Right(); 
        nickInput.text = string.Empty; 
    }

    //! Handle the action when the start button is clicked
    private void OnStartButtonClick()
    {
        startMenu.GetComponent<UIAnimator>().Left(); 
        gameInput.GetComponent<UIAnimator>().Left(); 
    }

    //! Handle the action when the quit button is clicked
    private void OnQuitButtonClick()
    {
        gameInput.SetActive(false); 
        Application.Quit();  
        Debug.Log("app closed"); 
    }

    //! Handle the action when the play button is clicked
    private void PlayButtonClick()
    {
        if (nickInput.text.Length < 1)
            return;

        GameData.Instance.Username = nickInput.text; 
        gameInput.GetComponent<UIAnimator>().Left();
        difficultyMenu.GetComponent<UIAnimator>().Left();
    }
}

