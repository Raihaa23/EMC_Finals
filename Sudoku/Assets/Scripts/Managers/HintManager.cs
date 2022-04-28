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
        
        private int _currentHintCount;
        private int _maxHintCount;
        private SaveManager _saveManager;

        private const int EasyHintCount = 3;
        private const int MediumHintCount = 5;
        private const int HardHintCount = 8;
        
        private void Start()
        {
            _saveManager = SaveManager.Instance;


            var gameMode = GameSettings.Instance.GetGameMode();

            var hintCount = 0;
            
            switch (gameMode)
            {
                case GameModeDifficulty.Easy:
                    hintCount = EasyHintCount;
                    break;
                case GameModeDifficulty.Unknown:
                    hintCount = 0;
                    break;
                case GameModeDifficulty.Medium:
                    hintCount = MediumHintCount;
                    break;
                case GameModeDifficulty.Hard:
                    hintCount = HardHintCount;
                    break;
                default:
                    hintCount = 0;
                    break;
            }

            _currentHintCount = hintCount;
            
            if (_saveManager.Data.RemainingHints > 0)
            {
                _currentHintCount = _saveManager.Data.RemainingHints;
            }
            
            _maxHintCount = hintCount;
        }

        private void Update()
        {
            hintCountTxt.text = _currentHintCount.ToString();
            maxHintTxt.text = _maxHintCount.ToString();
        }

        public void UseHint()
        {
            if (_currentHintCount > 0)
            {
                GameEvents.OnGetHintMethod();
            }

            SaveManager.Instance.Data.UpdateRemainingHints(_currentHintCount);
            SaveManager.Instance.Save();
        }

        private void ReduceHints()
        {
            _currentHintCount -= 1;
            SaveManager.Instance.Data.UpdateRemainingHints(_currentHintCount);
            SaveManager.Instance.Save();
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