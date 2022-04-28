using UnityEngine;

namespace GridManagement
{
    
    
    public class GameSettings : MonoBehaviour
    {
     
        #region singleton
        public static GameSettings Instance;

        void Awake()
        {
            _Paused = false;
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
                Destroy(this);
        }

        #endregion

        private GameModeDifficulty _gameModeDifficulty;
        private bool _Paused = false;

        public void SetPaused(bool paused) { _Paused = paused;}
        public bool GetPaused() { return _Paused;}

        void Start()
        {
            _gameModeDifficulty = GameModeDifficulty.Unknown;
        }

        public void SetGameMode(GameModeDifficulty mode)
        {
            _gameModeDifficulty = mode; 
        }

        public GameModeDifficulty GetGameMode()
        {

            return _gameModeDifficulty;
        }
    }
}
