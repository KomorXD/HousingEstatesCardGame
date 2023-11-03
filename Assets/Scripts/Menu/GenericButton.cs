using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

//! Class managing generic button hover animations
public class GenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleChange = 0.1f;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = GetComponent<RectTransform>().localScale;
    }
    
    //! On pointer enter event handler, scales button down
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(initialScale - Vector3.one * scaleChange, animationDuration).SetEase(Ease.Linear);
    }

    //! On pointer exit event handler, scales button to it's default state
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(initialScale, animationDuration).SetEase(Ease.Linear);
    }
}
