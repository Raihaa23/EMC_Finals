using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridSquare : Selectable
{

    [SerializeField] private TMP_Text number_text;
    
    private int number_ = 0;

    public void DisplayText()
    {
        if (number_ <= 0)
            number_text.GetComponent<TMP_Text>().text = " ";
        else
            number_text.GetComponent<TMP_Text>().text = number_.ToString();
    }

    public void SetNumber(int number)
    {
        number_ = number;
        DisplayText();
    }
}
