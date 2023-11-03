using System.Collections.Generic;
using TMPro;
using UnityEngine;

//! Class responsible for managing current points UI element
public class PointsHUDManager : MonoBehaviour, IHUDManager
{
    private TMP_Text textarea;

    private void Awake()
    {
        textarea = GetComponentInChildren<TMP_Text>();
    }

    //! Updates UI
    public void Init()
    {
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

    //! Updates points list with new values and indicators
    public void UpdateUI()
    {
        Dictionary<ParameterCategory, float> points = GameManager.Instance.GameParameters;
        List<DifficultyRequirement> requirements = GameManager.Instance.GameDifficulty.Requirements;

        textarea.text = "Points:\n";

        foreach (var point in points)
        {
            DifficultyRequirement diff = requirements.Find((req) => req.Parameter.Category == point.Key);

            if(diff != default)
            {
                if(point.Value < diff.Min)
                {
                    textarea.text += "[-] ";
                }
                else if(point.Value > diff.Max)
                {
                    textarea.text += "[+] ";
                }
                else
                {
                    textarea.text += "[O] ";
                }
            }

            textarea.text += $"{point.Key} - {point.Value}\n";
        }
    }
}
