using UnityEngine;

public class WalkAroundEstateState : IGameState
{
    private GameManager gm;
    private GameObject player;
    private CameraMoveScript cms;

    public WalkAroundEstateState(GameManager gameManager)
    {
        gm = gameManager;
        player = GameObject.FindGameObjectWithTag("Player");
        cms = player.GetComponentInChildren<CameraMoveScript>();
        cms.CameraMode = new FPSCameraMovement(cms);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gm.SetState(new MainState(gm));
        }
    }
}
