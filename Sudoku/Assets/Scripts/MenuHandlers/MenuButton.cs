using System;
using GridManagement;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuHandlers
{
    public class MenuButton : MonoBehaviour
    {


        [SerializeField]
        private GameObject continueButton;

        private SaveManager _saveManager;
        
        private void Start()
        {
            _saveManager = SaveManager.Instance;
            _saveManager.Load();

            if (_saveManager.HasSave())
            {
                if (continueButton == null)
                {
                    return;
                }
                continueButton.SetActive(true);
            }
        }

        public void OnContinueButtonPressed(string name)
        {
            var saveManager = SaveManager.Instance;
            saveManager.Load();
            var data = saveManager.Data;
            
            GameSettings.Instance.SetGameMode(data.GameModeDifficulty);
            SceneManager.LoadScene(name);
        }
        
        public void LoadScene(string name)
        {
            ResetCounter.Instance.resetCounted = 0;
            SceneManager.LoadScene(name);

        }

        public void LoadEasyGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameModeDifficulty.Easy);
            SceneManager.LoadScene(name);
            _saveManager.Clear();
            
        }
        public void LoadMediumGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameModeDifficulty.Medium);
            SceneManager.LoadScene(name);
            _saveManager.Clear();

        }
        public void LoadHardGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameModeDifficulty.Hard);
            SceneManager.LoadScene(name);
            _saveManager.Clear();

        }
        public void ActivateObject(GameObject obj)
        {
            obj.SetActive(true);
        }
        public void DeActivateObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        public void SetPause(bool paused)
        {
            GameSettings.Instance.SetPaused(paused);
        }

        public void Restart()
        {
            _saveManager.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }
}
