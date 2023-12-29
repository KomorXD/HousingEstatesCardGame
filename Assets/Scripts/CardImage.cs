using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardData cardData;
    private float posY;
    private Button button;

    public void Start()
    {
        posY = transform.position.y;
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectPlayerCard);
    }

    private void SelectPlayerCard()
    {
        Debug.Log(this.cardData.Value.ToString());
        Debug.Log(this.cardData.Color.ToString());
        GameManager.Instance.SelectedCard = cardData;
        GameHUDManager.Instance.UpdateUI();
    }

    public void Init(CardData cardData)
    {
        this.cardData = cardData;

        Texture2D cardTex = Resources.Load<Texture2D>($"Textures/Cards/{cardData.Value}{cardData.Color}");
        GetComponent<Image>().sprite = Sprite.Create(cardTex, new(0.0f, 0.0f, cardTex.width, cardTex.height), Vector2.zero);
    }

    //! On pointer enter event handler, moves card up
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOMove(new Vector3(transform.position.x, posY + 50, 0), 0.3f);
    }

    //! On pointer exit event handler, moves card down
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOMove(new Vector3(transform.position.x, posY, 0), 0.3f);
    }
}
