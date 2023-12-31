using UnityEngine;

//! State of the ongoing game
public class MainState : IGameState
{
    private GameManager gm;

    /**
     * Intializes internal state, destroys character controller, if it exists on a player
     * 
     * \param gameManager Parent game manager
     */
    public MainState(GameManager gameManager)
    {
        gm = gameManager;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CameraMoveScript cms = player.GetComponentInChildren<CameraMoveScript>();
        cms.CameraMode = new EditorCameraMovement(cms);

        gm.DrawRandomCard();
        GameHUDManager.Instance.SetActive(true);
        GameHUDManager.Instance.UpdateUI();

        GameObject.FindObjectOfType<BoardScript>().RemoveCollider();

        if(player.TryGetComponent<CharacterController>(out CharacterController controller))
        {
            GameObject.Destroy(controller);
        }
    }

    //! Checks if any bomb was used on a building
    public void Update()
    {
        if (!gm.BombsSelected || gm.AvailableBombs <= 0)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitDestructable = Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("CardBuilding");

        if (hitDestructable && Input.GetMouseButtonDown(0))
        {
            GameObject hitBuilding = hit.transform.gameObject;
            GameObject hitBuildingCard = hitBuilding.transform.parent.gameObject;
            GameObject hitBuildingTile = hitBuildingCard.transform.parent.gameObject;
            TileScript hitTileScript = hitBuildingTile.GetComponent<TileScript>();

            hitTileScript.ClearTile();
        }
    }
}
