using System;
using System.Collections.Generic;
using UnityEngine;

public struct DifficultyRequirement
{
    public float Min;
    public float Max;
    public CardParameter Parameter;

    public DifficultyRequirement(CardParameter parameter, float min, float max)
    {
        Parameter = parameter;
        Min = min;
        Max = max;
    }

    public static bool operator ==(DifficultyRequirement a, DifficultyRequirement b)
    {
        return a.Parameter.Category == b.Parameter.Category;
    }

    public static bool operator !=(DifficultyRequirement a, DifficultyRequirement b)
    {
        return !(a == b);
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
    public static DifficultiesManager Instance { get; private set; }

    private List<Difficulty> difficulties;
    public List<Difficulty> Difficulties => difficulties;

    public void Init()
    {
        difficulties = new();
        
        Difficulty diff = new Difficulty("Easy", "Difficulties/easy_icon", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.Trees, 10), 1, 100)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Normal", "Difficulties/normal_icon", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.GreenSpaceIndex, 11), 2, 200),
            new DifficultyRequirement(new CardParameter(ParameterCategory.DwellingsPerHa, 12), 3, 300)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Hard", "Difficulties/hard_icon", new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.FloorRatio, 13), 3, 300),
            new DifficultyRequirement(new CardParameter(ParameterCategory.AverageFloors, 14), 4, 400)
        });
        difficulties.Add(diff);
    }

    public void InitFromFile(string path)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        Instance = this;
    }
}
