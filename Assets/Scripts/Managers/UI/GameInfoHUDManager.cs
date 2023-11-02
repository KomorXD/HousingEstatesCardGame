using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameInfoHUDManager : MonoBehaviour, IHUDManager
{
    private Image icon;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text requirementsList;

    private void Awake()
    {
        icon = GetComponentInChildren<Image>();
    }

    public void Init()
    {
        Texture2D iconTex = Resources.Load<Texture2D>($"Textures/{GameManager.Instance.GameDifficulty.IconPath}");
        icon.sprite = Sprite.Create(iconTex, new(0.0f, 0.0f, iconTex.width, iconTex.height), Vector2.zero);

        nickname.text = PlayerPrefs.GetString("nickname") ?? "Placeholder";

        UpdateUI();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
    public void SetInteractive(bool interactive)
    {
        ;
    }

    public void UpdateUI()
    {
        List<DifficultyRequirement> requirements = GameManager.Instance.GameDifficulty.Requirements;
        requirementsList.text = "Requirements:\n";

        foreach (var requirement in requirements)
        {
            requirementsList.text += $"{requirement.Parameter.Category} [{requirement.Min} : {requirement.Max}]\n";
        }
    }
}
