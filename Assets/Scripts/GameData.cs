using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance {  get; private set; }

    [SerializeField] private Difficulty difficulty;
    public Difficulty GameDifficulty { get => difficulty; set => difficulty = value; }

    [SerializeField] private string username;
    public string Username { get => username; set => username = value; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
