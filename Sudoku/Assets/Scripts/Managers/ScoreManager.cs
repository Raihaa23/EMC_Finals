using GridManagement;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Singleton

        private static ScoreManager _instance;
        public static ScoreManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        private float _ongoingScore;
        private float _finalScore;
    
        [SerializeField] private float addScore = 10;
        [SerializeField] private float subtractScore = 5;

        [SerializeField] private float easyMultiplier = 1;
        [SerializeField] private float mediumMultiplier = 1.5f;
        [SerializeField] private float hardMultiplier = 2.5f;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI finalScoreText;

        private void Update()
        {
            scoreText.text = _ongoingScore.ToString();
        }

        public void AddScore()
        {
            switch (GameSettings.Instance.GetGameMode())
            {
                case "Easy":
                    _ongoingScore += addScore * easyMultiplier;
                    break;
                case "Medium":
                    _ongoingScore += addScore * mediumMultiplier;
                    break;
                case "Hard":
                    _ongoingScore += addScore * hardMultiplier;
                    break;
            }
        }

        public void reduceScore()
        {
            if (_ongoingScore > 0)
            {
                _ongoingScore -= subtractScore;
            }
            else
            {
                _ongoingScore = 0;
            }
        
        }

        public void ConvertToFinalScore()
        {
            _finalScore = _ongoingScore;
            finalScoreText.text = _finalScore.ToString();
        }
    }
}
