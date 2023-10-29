using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelAnimator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectToAnimate;

    public void Left()
    {
        foreach (GameObject obj in objectToAnimate)
        {
            obj.GetComponent<UIAnimator>().Left();
        }
    }

    public void Right()
    {
        foreach (GameObject obj in objectToAnimate)
        {
            obj.GetComponent<UIAnimator>().Right();
        }
    }
}
