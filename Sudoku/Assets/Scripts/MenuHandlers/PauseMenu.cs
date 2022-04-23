using Managers;
using TMPro;
using UnityEngine;

namespace MenuHandlers
{
    public class PauseMenu : MonoBehaviour
    {
        public TMP_Text time_text;

        public void DisplayTime()
        {
            time_text.text = Clock.instance.GetCurrentTimeText().text;
        }
    }
}
