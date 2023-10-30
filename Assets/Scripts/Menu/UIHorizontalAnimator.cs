using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIHorizontalAnimator : MonoBehaviour
{
    [SerializeField] private Ease easeType;
    [SerializeField] private Vector3 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private float delayLeft;
    [SerializeField] private float delayRight;

    private RectTransform objectToAnimate;
    private Vector2 startingPosition;
    private Vector2 currentPosition;
    private float positionDiff;

    private void Awake()
    {
        objectToAnimate = GetComponent<RectTransform>();
        startingPosition = objectToAnimate.localPosition;
        positionDiff = startingPosition.x - finalPosition.x;
        currentPosition = startingPosition;
    }

    public void Left()
    {
        currentPosition.x -= positionDiff;
        objectToAnimate.DOLocalMove(currentPosition, duration).SetEase(easeType).SetDelay(delayLeft);
    }

    public void Right()
    {
        currentPosition.x += positionDiff;
        objectToAnimate.DOLocalMove(currentPosition, duration).SetEase(easeType).SetDelay(delayRight);
    }
}