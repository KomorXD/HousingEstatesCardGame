using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private GameObject _card;
    private Vector3 _cardPosition;

    public GameObject cardToDisplay;

    private void Start()
    {
        _cardPosition = transform.position;
        _cardPosition.y += 0.01f;
    }

    private void OnMouseDown()
    {
        if (_card)
            return;
        DisplayCard(cardToDisplay);
    }

    public void DisplayCard(GameObject card)
    {
        GameObject _env = GameObject.FindGameObjectWithTag("BoardTag");
        _card = card;
        _card.transform.position = _cardPosition;
        _card = Instantiate(card, _env.transform);
    }
}
