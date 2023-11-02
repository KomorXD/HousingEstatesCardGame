using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonScript : MonoBehaviour
{
    private string buttonName;
    private string iconPath;

    public TextMeshProUGUI textMeshProUGUI;
    public RawImage rawImage;

    public void Init(string name, string path)
    {
        buttonName = name;
        iconPath = path;
        Texture2D texture = Resources.Load<Texture2D>($"{iconPath}{buttonName}");
        textMeshProUGUI.text = buttonName.ToUpper();
        rawImage.texture = texture;
    }
}
