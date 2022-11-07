using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private AudioSource audioSource;
    private float timeElapsed;
    [SerializeField] private TMP_Text timerClock;

    private int minutes, seconds, cents;

    // Start is called before the first frame update
    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - minutes * 60f);
        cents = (int)((timeElapsed - (int)timeElapsed) * 1000f);
        timerClock.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, cents);

    }

    public static void PausedSound()
    {
        instance.audioSource.Pause();
    }

    public static void UnPausedSound()
    {
        instance.audioSource.UnPause();
    }
}
