using System.Collections.Generic;
using UnityEngine;

public class MainState : IGameState
{
    private GameManager gm;

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

    public void Update()
    {
        if (!gm.BombsSelected)
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
