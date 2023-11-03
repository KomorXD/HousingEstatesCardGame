using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Handles;

public class DifficultyButtonScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public RawImage rawImage;

    private Difficulty difficulty;

    public void Init(Difficulty diff)
    {
        difficulty = diff;

        Texture2D texture = Resources.Load<Texture2D>(diff.IconPath);
        textMeshProUGUI.text = diff.Name.ToUpper();
        rawImage.texture = texture;
    }

    public void SelectDifficulty()
    {
        GameData.Instance.GameDifficulty = difficulty;
    }
}