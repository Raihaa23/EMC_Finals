using Events;
using GridManagement;
using UnityEngine;

namespace Managers
{
    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject victoryPopUp;

        private SaveManager _saveManager;
        private int correctCount = 0;

        private int currentChecker;
    
        private int easyChecker = 30;
        private int mediumChecker = 45;
        private int hardChecker = 65;
        

        private void Start()
        {
            _saveManager = SaveManager.Instance;

            if (_saveManager.HasSave())
            {
                correctCount = _saveManager.Data.CorrectCount;
            }
            
            switch (GameSettings.Instance.GetGameMode())
            {
                case GameModeDifficulty.Easy:
                    currentChecker = easyChecker;
                    break;
                case GameModeDifficulty.Medium:
                    currentChecker = mediumChecker;
                    break;
                case GameModeDifficulty.Hard:
                    currentChecker = hardChecker;
                    break;
            }
        }

        private void CorrectNumber(int squareIndex)
        {
            correctCount += 1;
            _saveManager.Data.CorrectCount = correctCount;
            _saveManager.Save(showLogs:true);
            CheckForVictory();
        }
        private void CheckForVictory()
        {
            if (correctCount == currentChecker)
            {
                GameEvents.OnVictoryMethod();
                ScoreManager.Instance.ConvertToFinalScore();
                victoryPopUp.SetActive(true);
            }
        }
    
        private void OnEnable()
        {
            GameEvents.OnCorrectNumber += CorrectNumber;
        }
    
        private void OnDisable()
        {
            GameEvents.OnCorrectNumber -= CorrectNumber;
        }
    }
}
