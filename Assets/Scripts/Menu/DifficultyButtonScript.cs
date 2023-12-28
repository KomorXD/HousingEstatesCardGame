using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//! A class responsible for managing a difficulty selection button behavior
public class DifficultyButtonScript : MonoBehaviour
{
    //! Text element for displaying difficulty name
    public TextMeshProUGUI textMeshProUGUI;
    //! RawImage for displaying the difficulty icon
    public RawImage rawImage;

    //! The selected difficulty data
    private Difficulty difficulty; 

    //! Initializes the difficulty button with a specific difficulty
    public void Init(Difficulty diff)
    {
        difficulty = diff;
        Texture2D texture = Resources.Load<Texture2D>(diff.IconPath);
        textMeshProUGUI.text = diff.Name.ToUpper();
        rawImage.texture = texture;
    }

    //! Called when the difficulty button is clicked, selects the difficulty in GameData
    public void SelectDifficulty()
    {
        GameData.Instance.GameDifficulty = difficulty;
    }

    //! Called when the difficulty button is clicked, starts the game
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }
}
