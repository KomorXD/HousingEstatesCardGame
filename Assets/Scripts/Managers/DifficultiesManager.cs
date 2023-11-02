using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DifficultyRequirement
{
    public CardParameter Parameter;
    public float Min;
    public float Max;

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

[System.Serializable]
public struct Difficulty
{
    public string Name;
    public string IconPath;
    public int BombsCount;
    public List<DifficultyRequirement> Requirements;

    public Difficulty(string name, string iconPath, int bombsCount, List<DifficultyRequirement> reqs)
    {
        Name = name;
        IconPath = iconPath;
        BombsCount = bombsCount;
        Requirements = reqs;
    }
}

public class DifficultiesManager : MonoBehaviour
{
    public static DifficultiesManager Instance { get; private set; }

    [SerializeField] private List<Difficulty> difficulties;
    public List<Difficulty> Difficulties => difficulties;

    public void Init()
    {
        difficulties = new();
        
        Difficulty diff = new Difficulty("Easy", "Textures/Difficulties/easy_icon", 10, new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.Trees, 10), 1, 100)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Normal", "Textures/Difficulties/normal_icon", 5, new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.GreenSpaceIndex, 11), 2, 200),
            new DifficultyRequirement(new CardParameter(ParameterCategory.DwellingsPerHa, 12), 3, 300)
        });
        difficulties.Add(diff);

        diff = new Difficulty("Hard", "Textures/Difficulties/hard_icon", 2, new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.FloorRatio, 13), 3, 300),
            new DifficultyRequirement(new CardParameter(ParameterCategory.AverageFloors, 14), 4, 400)
        });
        difficulties.Add(diff);

        //diff = new Difficulty("VHard", "Textures/Difficulties/", new()
        //{
        //    new DifficultyRequirement(new CardParameter(ParameterCategory.FloorRatio, 13), 3, 300),
        //    new DifficultyRequirement(new CardParameter(ParameterCategory.AverageFloors, 14), 4, 400)
        //});
        //difficulties.Add(diff);
    }

    public void InitFromFile()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;
        Init();
        DontDestroyOnLoad(gameObject);
    }
}
