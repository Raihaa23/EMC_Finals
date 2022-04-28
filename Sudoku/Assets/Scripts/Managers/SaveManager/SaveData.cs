using System.Collections.Generic;
using GridManagement;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{



    public struct ElapsedTime
    {
        public int Hours {get; set;}
        public int Minutes {get; set;}
        public int Seconds {get; set;}
    }
    
    
    
    /// <summary>
    /// 1. board data
    /// 2. board answers
    /// 3. current score
    /// 4. hint
    /// 5. time
    /// </summary>
    public class SaveData
    {
        public int RemainingHints = 0;
        public float CurrentScore = 0;
        public int CorrectCount = 0;
        public float TotalElapsedTimeInSeconds = 0f;
        public int TotalResetCount = 0;


        public List<int> CurrentGridData = new List<int>();
        public int GameLevel = -1;
        public GameModeDifficulty GameModeDifficulty = GameModeDifficulty.Unknown;
        
        public delegate void OnUpdateCallback();
        public event OnUpdateCallback OnUpdate;



        public void UpdateGameLevel(int gameLevel)
        {
            GameLevel = gameLevel;
        }
        
        public void UpdateRemainingHints(int hints)
        {
            RemainingHints = hints;
            OnUpdate?.Invoke();
            
        }

        public void UpdateCurrentScore(float score)
        {
            CurrentScore = score;
            OnUpdate?.Invoke();
        }

        public void UpdateTotalElapsedTime(float time)
        {
            TotalElapsedTimeInSeconds = time;
            OnUpdate?.Invoke();
        }

        public void UpdateTotalResetCount(int resetCount)
        {
            TotalResetCount = resetCount;
            OnUpdate?.Invoke();
        }

        public void Reset()
        {
            RemainingHints = 0;
            CurrentScore = 0;
            TotalElapsedTimeInSeconds = 0f;
            
            TotalResetCount = 0;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static SaveData FromJson(string json)
        {   
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}