using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Resources and Controls")]
    [Header("---------------------------")]
    [SerializeField] private AudioSource audioSource;
    private float timeElapsed;

    [Header("---------------------------")]
    [Header("Health Bar Components")]
    [SerializeField] private TMP_Text texttimerClock;
    [SerializeField] private TMP_Text textLifes;
    [SerializeField] private TMP_Text textHealth;
    [SerializeField] private TMP_Text textCoins;
    [SerializeField] private TMP_Text textMaps;


    [Header("---------------------------")]
    [Header("Collected Items")]
    public int collectedMaps = 0;
    public int collectedCoins = 0;

    [Header("---------------------------")]
    [Header("Health Bar Components")]
    [SerializeField] private Image imageHealthBar;
    public float maxHealthPlayer;
    public float healthCurrentPlayer;
    public int lifesPlayer;
    public bool isDead;

    [Header("---------------------------")]
    [Header("Pause Menu")]
    public bool isPaused;
    public GameObject pauseMenu;
    public GameObject hudMenu;

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
        maxHealthPlayer = 100;
        healthCurrentPlayer = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!isDead) && (!isPaused))
        {
            ClockRunning();
            MenuValues();
            
        }

        TogglePause();

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
        textCoins.text = "X " + Convert.ToString(collectedCoins);
        textMaps.text = Convert.ToString(collectedMaps) + " X";
        imageHealthBar.fillAmount = healthCurrentPlayer / maxHealthPlayer; 
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

    public void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        hudMenu.SetActive(false);
        Time.timeScale = 0; // Tambien puede relentizar el juego (ejemplo: 0.01f)
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        hudMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
