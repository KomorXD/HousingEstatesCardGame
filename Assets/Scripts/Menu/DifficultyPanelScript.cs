using System.Collections.Generic;
using UnityEngine;

public class DifficultyPanelScript : MonoBehaviour
{
    //! List to store difficulty options
    private List<Difficulty> difficulties;

    //! Variables to keep track of the current row for button placement.
    private int currentRow = 0;
    //! Variables to keep track of the current column for button placement
    private int currentCol = 0;

    //! Initial position for the first difficulty button
    private Vector3 firstButtonPos = new Vector3(-300, 150, 0);

    //! Instantiate button for every defined difficulty
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

    //! Calculate the starting position for the next button
    private Vector3 CalcualteStartPos(Vector3 startPos)
    {
        startPos.y -= (currentRow * startPos.y);
        startPos.x += Mathf.Abs(currentCol * startPos.x);
        return startPos;
    }

    //! Update the current row and column for button placement
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

