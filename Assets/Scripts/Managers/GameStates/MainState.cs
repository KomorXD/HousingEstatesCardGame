using System.Collections.Generic;
using UnityEngine;

public class MainState : IGameState
{
    private GameManager gm;

    public MainState(GameManager gameManager)
    {
        gm = gameManager;

        gm.AvailableBombs = 5;
        gm.SelectedCard = null;

        gm.Deck = new List<CardData>
        {
            new CardData(CardColor.Hearts, CardValue.Queen, new Vector2(3, 2), new()),
            new CardData(CardColor.Diamonds, CardValue.King, new Vector2(2, 2), new()),
            new CardData(CardColor.Spades, CardValue.Eight, new Vector2(2, 1), new()),
            new CardData(CardColor.Clubs, CardValue.Seven, new Vector2(3, 1), new())
        };

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CameraMoveScript cms = player.GetComponentInChildren<CameraMoveScript>();
        cms.CameraMode = new EditorCameraMovement(cms);

        gm.DrawRandomCard();
        GameHUDManager.Instance.Init();
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
