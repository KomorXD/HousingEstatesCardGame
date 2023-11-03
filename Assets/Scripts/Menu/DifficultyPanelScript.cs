using System.Collections.Generic;
using UnityEngine;

public class DifficultyPanelScript : MonoBehaviour
{
    private List<Difficulty> difficulties;

    private int currentRow = 0;
    private int currentCol = 0;

    private Vector3 firstButtonPos = new Vector3(-300, 150, 0);

    void Start()
    {
        difficulties = DifficultiesManager.Instance.Difficulties;

        Vector3 startPos;

        foreach (var difficulty in difficulties)
        {
            startPos = CalcualteStartPos(firstButtonPos);

            GameObject diffButton = Resources.Load<GameObject>("Prefabs/Menu/DifficultyButtonUI");
            diffButton = Instantiate(diffButton, this.transform);
            diffButton.name = $"{difficulty.Name}Button";
            diffButton.GetComponent<DifficultyButtonScript>().Init(difficulty);        
            diffButton.GetComponent<RectTransform>().localPosition = startPos;      

            CalculateRowCol();
        }
    }

    private Vector3 CalcualteStartPos(Vector3 startPos)
    {
        startPos.y -= (currentRow * startPos.y);
        startPos.x += Mathf.Abs(currentCol * startPos.x);
        return startPos;
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
