using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonScript : MonoBehaviour
{
    private string Name;
    private string IconPath;

    public TextMeshProUGUI textMeshProUGUI;
    public RawImage rawImage;

    public void Start()
    {
        Name = "Easy";
        IconPath = "Textures/Difficulties/";
        Init();
    }

    public void Init()
    {
        Texture2D texture = Resources.Load<Texture2D>($"{IconPath}{Name}");
        textMeshProUGUI.text = Name.ToUpper();
        rawImage.texture = texture;
    }
}
