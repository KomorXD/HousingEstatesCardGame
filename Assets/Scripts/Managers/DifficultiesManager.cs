using System;
using System.Collections.Generic;
using UnityEngine;

//! Struct holding a parameter's requirement for a difficulty
[System.Serializable]
public struct DifficultyRequirement
{
    //! Card parameter
    public CardParameter Parameter;

    //! Minimum required points
    public float Min;

    //! Maximum allowed points
    public float Max;

    //! Assigns values
    public DifficultyRequirement(CardParameter parameter, float min, float max)
    {
        Parameter = parameter;
        Min = min;
        Max = max;
    }

    //! Checks for equality
    public static bool operator ==(DifficultyRequirement a, DifficultyRequirement b)
    {
        return a.Parameter.Category == b.Parameter.Category;
    }

    //! Checks for !equality
    public static bool operator !=(DifficultyRequirement a, DifficultyRequirement b)
    {
        return !(a == b);
    }
}

//! Holds difficulty's data
[System.Serializable]
public struct Difficulty
{
    //! Difficulty's name
    public string Name;

    //! Difficultie's icon path
    public string IconPath;

    //! Difficultie's bombs count
    public int BombsCount;

    //! Dofficulties requirements for each parameter
    public List<DifficultyRequirement> Requirements;

    //! Assigns values
    public Difficulty(string name, string iconPath, int bombsCount, List<DifficultyRequirement> reqs)
    {
        Name = name;
        IconPath = iconPath;
        BombsCount = bombsCount;
        Requirements = reqs;
    }
}

//! Persistent object, holding defined difficulties
public class DifficultiesManager : MonoBehaviour
{
	//! Singleton instance
    public static DifficultiesManager Instance { get; private set; }

    [SerializeField] private List<Difficulty> difficulties;
    //! Defined difficulties list
    public List<Difficulty> Difficulties => difficulties;

    //! Intializes difficulties defined in code
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

        diff = new Difficulty("VHard", "Textures/Difficulties/VHard", 2, new()
        {
            new DifficultyRequirement(new CardParameter(ParameterCategory.FloorRatio, 13), 3, 300),
            new DifficultyRequirement(new CardParameter(ParameterCategory.AverageFloors, 14), 4, 400)
        });
        difficulties.Add(diff);
    }

    /**
     * [NOT IMPLEMENTED] Intializes difficulties defined in file
     * 
     * \param path Path to a file containing difficulties definitions
     */
    public void InitFromFile(string path)
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
