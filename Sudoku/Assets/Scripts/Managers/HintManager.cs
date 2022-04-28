using System;
using Events;
using GridManagement;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hintCountTxt;
        [SerializeField] private TextMeshProUGUI maxHintTxt;
        private int currentHintCount;
        private int maxHintCount;

        private int easyHintCount = 3;
        private int mediumHintCount = 5;
        private int hardHintCount = 8;
        private void Start()
        {
            switch (GameSettings.Instance.GetGameMode())
            {
                case "Easy":
                    currentHintCount = easyHintCount;
                    break;
                case "Medium":
                    currentHintCount = mediumHintCount;
                    break;
                case "Hard":
                    currentHintCount = hardHintCount;
                    break;
            }

            maxHintCount = currentHintCount;
        }

        private void Update()
        {
            hintCountTxt.text = currentHintCount.ToString();
            maxHintTxt.text = maxHintCount.ToString();
        }

        public void UseHint()
        {
            if (currentHintCount > 0)
            {
                GameEvents.OnGetHintMethod();
            }
        }

        private void ReduceHints()
        {
            currentHintCount -= 1;
        }

        private void OnEnable()
        {
            GameEvents.OnReduceHint += ReduceHints;
        }

        private void OnDisable()
        {
            GameEvents.OnReduceHint -= ReduceHints;
        }
    }
}