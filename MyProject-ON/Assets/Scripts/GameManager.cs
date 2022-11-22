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
    [SerializeField] private TMP_Text texttimerClock;
    [SerializeField] private TMP_Text textLifes;
    [SerializeField] private TMP_Text textHealth;

    //[SerializeField]
    public float maxHealthPlayer;
    //[SerializeField]
    public float healthCurrentPlayer;
    //[SerializeField]
    public int lifesPlayer;
    //[SerializeField]
    //public int continuesPlayer;
    //[SerializeField]
    public int collectedMaps = 0;
    //[SerializeField]
    public int collectedCoins = 0;
    public bool isDead;
    public bool isPaused;

    // Time Elapsed
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
        lifesPlayer = 3;
        //continuesPlayer = 2;
        maxHealthPlayer = 10;
        healthCurrentPlayer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!isDead) && (!isPaused))
        {
            ClockRunning();
            MenuValues();
        }

    }

    private void ClockRunning()
    {
        timeElapsed += Time.deltaTime;
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - minutes * 60f);
        cents = (int)((timeElapsed - (int)timeElapsed) * 1000f);
        texttimerClock.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, cents);
    }

    private void MenuValues()
    {
        textLifes.text = Convert.ToString(lifesPlayer);
        textHealth.text = Convert.ToString(Math.Round(healthCurrentPlayer,2));
    }

    // ---------------------------------
    /// <summary>
    /// Sound Player
    /// </summary>
    public static void PausedSound()
    {
        instance.audioSource.Pause();
    }

    public static void UnPausedSound()
    {
        instance.audioSource.UnPause();
    }
    // ---------------------------------


    
    
}
