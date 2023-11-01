using System;
using System.Collections.Generic;
using UnityEngine;

public struct DifficultyRequirement
{
    CardParameter Parameter;
    public float Min;
    public float Max;

    public DifficultyRequirement(CardParameter parameter, float min, float max)
    {
        Parameter = parameter;
        Min = min;
        Max = max;
    }
}

public struct Difficulty
{
    public string Name;
    public string IconPath;
    public List<DifficultyRequirement> Requirements;

    public Difficulty(string name, string iconPath, List<DifficultyRequirement> reqs)
    {
        Name = name;
        IconPath = iconPath;
        Requirements = reqs;
    }
}

public class DifficultiesManager : MonoBehaviour
{
    public DifficultiesManager Instance { get; private set; }

    private List<Difficulty> difficulties;
    public List<Difficulty> Difficulties => difficulties;

    public void Init()
    {
        difficulties = new();
        
        Difficulty diff = new Difficulty("Easy", "icons/easy_diff", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.Trees, 10), 1, 100)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Normal", "icons/normal_diff", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.GreenSpaceIndex, 11), 2, 200),
            new DifficultyRequirement(new CardParameter(ParameterCategory.DwellingsPerHa, 12), 3, 300)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Hard", "icons/hard_diff", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.FloorRatio, 13), 3, 300),
            new DifficultyRequirement(new CardParameter(ParameterCategory.AverageFloors, 14), 4, 400)
        });
        difficulties.Add(diff);
    }

    public void InitFromFile()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        Instance = this;
    }
}
