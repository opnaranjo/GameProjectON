using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


    //[Header("---------------------------")]
    //[Header("Pause Menu")]
    //public GameObject pauseMenu;
    //public GameObject pauseMenuExit;
    //public GameObject hudMenu;
    //public TMP_Text textPauseMessage;
    //public GameObject buttonRestart;

    private GameManager gameManager;
    //private int numScene;

    private void Start()
    {

        //gameManager = FindObjectOfType<GameManager>();

    }

    /*
    private void Update()
    {

        //gameManager = FindObjectOfType<GameManager>();
        if (gameManager.isPaused)
        {
            //PauseGame();
            Debug.Log("MenuController Pause!!!");
        }

    }
    */

    public void Play()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.isPaused = false;
        SceneManager.LoadScene(1);

    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


    public void MainMenu()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.isPaused = false;
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void PauseGame()
    {
        //pauseMenu.SetActive(true);
        //hudMenu.SetActive(false);
        //textPauseMessage.text = "Pause!!"; //pauseMessage;
        //buttonRestart.gameObject.SetActive(true);

        //gameManager.isPaused = true;
        //Debug.Log("Entra a GameManager.PauseGame.cs ----->>> " );
        Time.timeScale = 0; // Tambien puede relentizar el juego (ejemplo: 0.01f)

    }

    public void ResumeGame()
    {
        //pauseMenu.SetActive(false);
        //pauseMenuExit.SetActive(false);
        //hudMenu.SetActive(true);
        //gameManager.isPaused = false;
        Time.timeScale = 1;
    }
}
