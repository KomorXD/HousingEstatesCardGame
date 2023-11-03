using UnityEngine;

//! Persistent object holding game data
public class GameData : MonoBehaviour
{
	//! Singletone instance
    public static GameData Instance {  get; private set; }

    [SerializeField] private Difficulty difficulty;

    //! Selected difficulty
    public Difficulty GameDifficulty { get => difficulty; set => difficulty = value; }

    [SerializeField] private string username;

    //! Entered plauer name
    public string Username { get => username; set => username = value; }

    [SerializeField] private bool playerWon;

    //! Whether a player won
    public bool PlayerWon { get => playerWon; set => playerWon = value; }

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
