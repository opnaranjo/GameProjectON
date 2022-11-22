using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{

    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.isPaused = true;
    }
    

    public void Play()
    {
        SceneManager.LoadScene(1);
        gameManager.isPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
