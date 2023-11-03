using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIAnimator : MonoBehaviour
{
    //! The ease type for the animation
    [SerializeField] private Ease easeType;
    //! The final x position to animate to
    [SerializeField] private float finalX;
    //! The duration of the animation
    [SerializeField] private float duration;
    //! The delay before animating left
    [SerializeField] private float delayLeft;
    //! The delay before animating right
    [SerializeField] private float delayRight;

    //! Reference to the RectTransform to animate
    private RectTransform objectToAnimate;
    //! The starting position of the RectTransform
    private Vector2 startingPosition;
    //! The current position of the RectTransform
    private Vector2 currentPosition;
    //! The difference between starting and final x positions
    private float positionDiff; 

    //! Called when the object is awakened
    private void Awake()
    {
        objectToAnimate = GetComponent<RectTransform>(); 
        startingPosition = objectToAnimate.localPosition;
        positionDiff = startingPosition.x - finalX;
        currentPosition = startingPosition; 
    }

    //! Animate the object to the left
    public void Left()
    {
        currentPosition.x -= positionDiff; 
        objectToAnimate.DOLocalMove(currentPosition, duration).SetEase(easeType).SetDelay(delayLeft); 
    }

    //! Animate the object to the right
    public void Right()
    {
        currentPosition.x += positionDiff; 
        objectToAnimate.DOLocalMove(currentPosition, duration).SetEase(easeType).SetDelay(delayRight); 
    }
}

