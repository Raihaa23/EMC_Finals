using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Singleton

        private static SaveManager _instance;
        public static SaveManager Instance => _instance;

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
        
        public SaveData Data
        {
            get;
            private set;
        }

        /// <summary>
        /// This will be used for the key where the save data
        /// will be stored.
        /// </summary>
        private const string PlayerPrefsKey = "save_data";

        
        private void Start()
        {
            Load();
        }


        /// <summary>
        /// Saves the current game state
        /// </summary>
        public void Save(bool showLogs = false)
        {

            if (showLogs)
            {
                Debug.Log($"SAVING: {Data.ToJson()}");
            }

            PlayerPrefs.SetString(PlayerPrefsKey, Data.ToJson());
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
            Load();
            
        }

        public bool HasSave()
        {
            if (Data == null) return false;
            
            return Data.GameModeDifficulty != GameModeDifficulty.Unknown;
        }


        /// <summary>
        /// Loads and parses the saved json string
        /// </summary>
        public void Load(bool showLogs = false)
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey) == false)
            {
                Data = new SaveData();
                return;
            }
            
            var rawJson = PlayerPrefs.GetString(PlayerPrefsKey);
            Data = SaveData.FromJson(rawJson);
            
            if (showLogs)
            {
                Debug.Log($"LOADED: {Data.ToJson()}");
            }
        }
    }
}