using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameDataManager : MonoBehaviour
{

    private GameData gameData;
    private List<IGameData> gameDataObjects;

    public static GameDataManager instance {get; private set;}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Manager in Scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.gameDataObjects = FindAllGameDataObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load saved data
        //No saved data, start new game
        if(this.gameData == null)
        {
            Debug.Log("No data saved.");
            NewGame();
        }
        //Push saved to data to other scripts
        foreach(IGameData gameDataObj in gameDataObjects)
        {
            gameDataObj.LoadData(gameData);
        }

        Debug.Log("Loaded Score =" + gameData.IngameScore);

    }

    public void SaveGame()
    {
        // Pass data to other scripts to update
        foreach(IGameData gameDataObj in gameDataObjects)
        {
            gameDataObj.SaveData(ref gameData);
        }

        Debug.Log("Saved Score ="+ gameData.IngameScore);

        // Save data to a file
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IGameData> FindAllGameDataObjects()
    {
        IEnumerable<IGameData> gameDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IGameData>();

        return new List<IGameData>(gameDataObjects);
    }
}
