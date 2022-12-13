using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Resources and Controls")]
    [SerializeField] private AudioSource audioSource;

    [Header("---------------------------")]
    [Header("Canvas Player Values")]
    [SerializeField] private TMP_Text textClock;
    [SerializeField] private TMP_Text textNumLifes;
    [SerializeField] private TMP_Text textNumHealth;
    [SerializeField] private TMP_Text textCoins;
    [SerializeField] private TMP_Text textMaps;
    //[SerializeField] private Image imgHealthFront;
    //[SerializeField] private GameObject healthBarUI;
    [SerializeField] private UnityEngine.UI.Slider sliderHealth;

    [Header("---------------------------")]
    [Header("Player Values")]
    public float maxHealthPlayer;
    public float healthPlayer;
    public int lifesPlayer;
    public bool isDead;
    public bool isPaused;
    public int numScene;

    [Header("---------------------------")]
    [Header("Collected Items")]
    public int collectedMaps = 0;
    public int collectedCoins = 0;


    // Time Elapsed
    private float timeElapsed;
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
        //lifesPlayer = 2;
        maxHealthPlayer = 100;

        healthPlayer = maxHealthPlayer;
        sliderHealth.value = CalculateHealth();

    }

    // Update is called once per frame
    void Update()
    {
        numScene = SceneManager.GetActiveScene().buildIndex;
        sliderHealth.value = CalculateHealth();

        /*
        if (healthPlayer < maxHealthPlayer)
        {
            healthBarUI.SetActive(true);
        }
        */

        if (healthPlayer > maxHealthPlayer)
        {
            healthPlayer = maxHealthPlayer;
        }

        if (healthPlayer < 0)
        {
            healthPlayer = 0;
        }

        if (healthPlayer <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        if (!isDead && numScene != 0)
        {
            TogglePauseGame();
        }

        if ((!isPaused) && (numScene != 0))
        {
            ClockRunning();
            MenuValues();

        }

        TogglePauseMusic();


    }

   
    // --------------------------------

    public void TogglePauseGame()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }

    }

    private void ClockRunning()
    {
        timeElapsed += Time.deltaTime;
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - minutes * 60f);
        cents = (int)((timeElapsed - (int)timeElapsed) * 1000f);
        textClock.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, cents);
    }

    private void MenuValues()
    {
        textNumLifes.text = Convert.ToString(lifesPlayer);
        textNumHealth.text = Convert.ToString(Math.Round(healthPlayer,2));
        textCoins.text = "X " + Convert.ToString(collectedCoins);
        textMaps.text = Convert.ToString(collectedMaps) + " X";
    }

    private float CalculateHealth()
    {
        return healthPlayer / maxHealthPlayer;
    }

    public void TogglePauseMusic()
    {
        if (Input.GetKey(KeyCode.P))
        {
            PausedSound();
        }

        if (Input.GetKey(KeyCode.U))
        {
            UnPausedSound();
        }
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
