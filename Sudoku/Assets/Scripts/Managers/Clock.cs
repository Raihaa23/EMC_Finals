using System;
using Events;
using GridManagement;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class Clock : MonoBehaviour
    {
        private float _deltaTime;
        private TMP_Text textClock;
        public TextMeshProUGUI FinishTime;
        private bool _stopClock = false;

        public static Clock instance;

        void Awake()
        {
            if (instance)
                Destroy(instance);

            instance = this;

            textClock = GetComponentInChildren<TMP_Text>();
            _deltaTime = 0;
        }

        void Start()
        {
            _stopClock = false; 
        }

        void Update()
        {
            var saveData = SaveManager.Instance.Data;
        
            
            if(GameSettings.Instance.GetPaused() == false && _stopClock == false)
            {
                if (saveData != null)
                {
                    /*
                 * Compute for the saved elapsed time 
                 */
                    _deltaTime = saveData.TotalElapsedTimeInSeconds;
                }
                
                _deltaTime += Time.deltaTime;
                
                TimeSpan span = TimeSpan.FromSeconds(_deltaTime);

                string hour = LeadingZero(span.Hours);
                string minute = LeadingZero(span.Minutes);
                string seconds = LeadingZero(span.Seconds);

                textClock.text = hour + ":" + minute + ":" + seconds;
                FinishTime.text = textClock.text;
                
                saveData.UpdateTotalElapsedTime(_deltaTime);
                SaveManager.Instance.Save();
            }
        }

        string LeadingZero(int n)
        {
            return n.ToString().PadLeft(2, '0');
        }

        public void OnGameOver()
        {
            _stopClock = true;
        }

        private void OnEnable()
        {
            GameEvents.OnVictory += OnGameOver;
        }
    
        private void OnDisable()
        {
            GameEvents.OnVictory -= OnGameOver;
        }

        public TMP_Text GetCurrentTimeText()
        {
            return textClock;
        }
    }
}
