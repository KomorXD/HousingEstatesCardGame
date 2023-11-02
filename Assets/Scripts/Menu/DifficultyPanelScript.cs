using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DifficultyPanelScript : MonoBehaviour
{
    private List<Difficulty> difficulties;

    private int currentRow = 0;
    private int currentCol = 0;

    private Vector3 globalStarPos = new Vector3(1920, 300, 0);
    private Vector3 globalEndPos = new Vector3(-600, 300, 0);

    void Start()
    {

        difficulties = GetComponent<DifficultiesManager>().Difficulties;

        Vector3 startPos;
        Vector3 endPos;

        foreach (var difficulty in difficulties)
        {
            startPos = CalcualteStartPos(globalStarPos);
            endPos = CalcualteEndPos(globalEndPos);

            GameObject diffButton = Resources.Load<GameObject>("Prefabs/Menu/DifficultyButton");
            diffButton.GetComponent<DifficultyButtonScript>().Init(difficulty.Name, difficulty.IconPath);

            diffButton.GetComponent<UIHorizontalAnimator>().Init(
                new Vector3(startPos.x, startPos.y, startPos.z), 
                new Vector3(endPos.x, endPos.y, endPos.z ), 
                0.5f, 0.3f - currentCol * 0.1f, 0.1f + currentCol * 0.1f
                );
            diffButton.name = $"{difficulty.Name}Button";
            diffButton = Instantiate(diffButton, this.transform);
            GetComponent<MenuPanelAnimator>().AddObject(diffButton);
            diffButton.GetComponent<RectTransform>().localPosition = startPos;

            CalculateRowCol();
        }
    }

    private Vector3 CalcualteStartPos(Vector3 startPos)
    {
        startPos.y -= (currentRow * startPos.y);
        return startPos;
    }

    private Vector3 CalcualteEndPos(Vector3 endPos)
    {
        endPos.x += Mathf.Abs(currentCol * endPos.x);
        endPos.y -= (currentRow * endPos.y);
        return endPos;
    }

    private void CalculateRowCol()
    {
        if (currentCol == 2)
        {
            currentCol = 0;
            currentRow += 1;
            return;
        }
        currentCol += 1;
    }
}
