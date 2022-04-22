using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPopUp;

    private int correctCount = 0;

    private int currentChecker;
    
    private int easyChecker = 30;
    private int mediumChecker = 45;
    private int hardChecker = 65;

    private void Start()
    {
        switch (GameSettings.Instance.GetGameMode())
        {
            case "Easy":
                currentChecker = easyChecker;
                break;
            case "Medium":
                currentChecker = mediumChecker;
                break;
            case "Hard":
                currentChecker = hardChecker;
                break;
        }
    }

    private void Update()
    {
        Debug.Log("Correct Answers = " + correctCount + " / " + currentChecker);
    }

    private void CorrectNumber()
    {
        correctCount += 1;
        
        CheckForVictory();
    }
    private void CheckForVictory()
    {
        if (correctCount == currentChecker)
        {
            GameEvents.OnVictoryMethod();
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
