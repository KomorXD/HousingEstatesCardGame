using UnityEngine;

//! State controlling walking areound estate
public class WalkAroundEstateState : IGameState
{
    private GameManager gm;
    private GameObject player;
    private CameraMoveScript cms;

    /**
     * Intializes state, adds CharacterController component to a player
     * 
     * \param gameManager Parent game manager
     */
    public WalkAroundEstateState(GameManager gameManager)
    {
        gm = gameManager;
        player = GameObject.FindGameObjectWithTag("Player");
        cms = player.GetComponentInChildren<CameraMoveScript>();
        player.AddComponent<CharacterController>();
        cms.CameraMode = new FPSCameraMovement(cms);
        
        GameHUDManager.Instance.SetActive(false);
        GameObject.FindObjectOfType<BoardScript>().SpawnCollider();
    }

    //! Checks for Escpae press, which leaves that state
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gm.SetState(new MainState(gm));
        }
    }
}
