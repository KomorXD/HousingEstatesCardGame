using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputTextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI placeholder;

    public void ClearText()
    {
        text.ClearMesh();
        placeholder.enabled = true;
    }

    public string GetText()
    {
        return text.text;
    }
}
