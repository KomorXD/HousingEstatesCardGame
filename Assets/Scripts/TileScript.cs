using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject cardToDisplay;

    private GameObject _card;
    private Vector3 _cardPosition;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _cardPosition = transform.position;
        _cardPosition.y += 0.01f;
        _meshRenderer = GetComponent<MeshRenderer>();
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

    private void OnMouseEnter()
    {
        _meshRenderer.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        _meshRenderer.material.color = Color.white;
    }
}
