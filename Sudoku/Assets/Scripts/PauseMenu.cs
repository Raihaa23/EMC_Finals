using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TMP_Text time_text;

    public void DisplayTime()
    {
        time_text.text = Clock.instance.GetCurrentTimeText().text;
    }
}
