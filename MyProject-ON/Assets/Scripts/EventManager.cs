using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] private float delayOnDead;

    [SerializeField] private UnityEvent onResume;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent onBlink;
    [SerializeField] private UnityEvent onGameOver;
    [SerializeField] private UnityEvent onOther;



    private GameManager gameManager;
    //public MenuController menuController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //menuController = GameObject.Find("MenuController");
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager.isPaused == true && gameManager.numScene != 0)
        {
            onPause.Invoke();
            Debug.Log("Invocando event onPause");
        }
        else
        {
            onResume.Invoke();
        }

        if (gameManager.healthPlayer <= 0 && gameManager.isDead)
        {

            //StartCoroutine(DeadPlayer(delayOnDead));
            Debug.Log("Invocando event onDead");

            onBlink.Invoke();
            Debug.Log("Invocando event onBlink");

            

            if (gameManager.lifesPlayer == 0)
            {
                onGameOver.Invoke();
                Debug.Log("Invocando event onResume");
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Invocando event onPause");
            onPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Invocando event onResume");
            onResume.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Invocando event onOther");
            onOther.Invoke();
        }
    }

    public IEnumerator DeadPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        onDead.Invoke();
        
    }

}
