using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleChange = 0.1f;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = GetComponent<RectTransform>().localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(initialScale - Vector3.one * scaleChange, animationDuration).SetEase(Ease.Linear);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(initialScale, animationDuration).SetEase(Ease.Linear);
    }
}
