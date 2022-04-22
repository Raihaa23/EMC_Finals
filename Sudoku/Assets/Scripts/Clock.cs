using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    private float delta_time;
    private TMP_Text textClock;
    private bool stop_clock_ = false;

    public static Clock instance;

    void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;

        textClock = GetComponentInChildren<TMP_Text>();
        delta_time = 0;
    }

    void Start()
    {
        stop_clock_ = false; 
    }

    void Update()
    {
        if(GameSettings.Instance.GetPaused() == false && stop_clock_ == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            textClock.text = hour + ":" + minute + ":" + seconds;
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOver()
    {
        stop_clock_ = true;
    }

    private void OnEnable()
    {
        GameEvents.OnVictory += OnGameOver;
    }
    
    private void OnDisable()
    {
        GameEvents.OnVictory -= OnGameOver;
    }

    public TMP_Text GetCurrentTimeText()
    {
        return textClock;
    }
}
