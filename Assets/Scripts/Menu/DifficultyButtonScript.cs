using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}