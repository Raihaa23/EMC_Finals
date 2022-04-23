using GridManagement;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuHandlers
{
    public class MenuButton : MonoBehaviour
    {
        public void LoadScene(string name)
        {
            ResetCounter.Instance.resetCounted = 0;
            SceneManager.LoadScene(name);

        }

        public void LoadEasyGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameSettings.EGameMode.EASY);
            SceneManager.LoadScene(name);
        }
        public void LoadMediumGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameSettings.EGameMode.MEDIUM);
            SceneManager.LoadScene(name);
        }
        public void LoadHardGame(string name)
        {
            GameSettings.Instance.SetGameMode(GameSettings.EGameMode.HARD);
            SceneManager.LoadScene(name);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
    }
}
