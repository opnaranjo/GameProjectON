using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PlayerInteraction : MonoBehaviour
{
    public float timer = 0f;
    public float limitTimer = 2f;
    public float limitFloor = -10;

    //public float blinkDelay = 1.25f;
    //public float blinkWait = 3.5f;
    public float waitHealing = 4.5f;
    //public float waitRespawn = 3.5f;

    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private Vector3 savedScale;
    private GameManager gameManager;
    private Rigidbody rb;
    private Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        SaveTransform();
        //Capture GameManager
        gameManager = FindObjectOfType<GameManager>();
        //RigidBody Player
        rb = GetComponent<Rigidbody>();
        renderers = GetComponentsInChildren<Renderer>();
        gameManager.isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.healthCurrentPlayer > 0)
        {
            gameManager.isDead = false;

            if (Input.GetKey(KeyCode.P))
            {
                GameManager.PausedSound();
            }

            if (Input.GetKey(KeyCode.U))
            {
                GameManager.UnPausedSound();
            }
        }
        else
        {
            gameManager.isDead = true;

            if (gameManager.lifesPlayer > 0)
            {
                Invoke("RestoreAllHealth", waitHealing);
                //StartCoroutine(Blink(blinkWait, blinkDelay));
                //Invoke("Respawn", waitRespawn);
                
            }
        }

        // Respawn player if fall 
        if (transform.position.y < limitFloor)
        {
            DamagePlayer(gameManager.maxHealthPlayer);
            Respawn();
            HealingPlayer(gameManager.maxHealthPlayer);
        }


        if (gameManager.lifesPlayer == 0)
        {
            Debug.Log("Game Over");
        }

    }
    
    void OnTriggerEnter(Collider col)
    {
        if (!gameManager.isDead)
        {
            // Nombre del GameObject
            //Debug.Log("GameObject: " + col.name);

            if (col.transform.gameObject.tag == "Coin")
            {

                Destroy(col.transform.gameObject);
                gameManager.collectedCoins++;
            }

            if (col.transform.gameObject.tag == "Map")
            {

                Destroy(col.transform.gameObject);
                gameManager.collectedMaps++;
            }

            if (col.transform.gameObject.tag == "Trap")
            {
                DamagePlayer();
            }

            if (col.transform.gameObject.tag == "Enemy")
            {

                DamagePlayer();
            }
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (!gameManager.isDead)
        {
            timer += Time.deltaTime;
            if (timer >= limitTimer)
            {
                if (col.transform.gameObject.tag == "Portal1")
                {
                    SceneManager.LoadScene(1);
                }

                if (col.transform.gameObject.tag == "Portal2")
                {
                    SceneManager.LoadScene(0);
                }

                timer = 0;
            }
        }
    }

    IEnumerator Blink(float waitTime, float delayBlink)
    {
        yield return new WaitForSeconds(waitTime);
        
        foreach (var rend in renderers)
        {
            rend.enabled = false;

        }
        yield return new WaitForSeconds(delayBlink);
        foreach (var rend in renderers)
        {
            rend.enabled = true;

        }
        yield return new WaitForSeconds(delayBlink);

    } 
        
    void RestoreAllHealth()
    {
        gameManager.healthCurrentPlayer = gameManager.maxHealthPlayer;
    }

    void HealingPlayer() // Healing by one
    {
        if (gameManager.lifesPlayer > 0)
        {
            gameManager.healthCurrentPlayer++;
            if (gameManager.healthCurrentPlayer > gameManager.maxHealthPlayer)
            {
                gameManager.healthCurrentPlayer = gameManager.maxHealthPlayer;
            }
        }

    }

    void HealingPlayer(float amounHealing)
    {
        if (gameManager.lifesPlayer > 0)
        {
            gameManager.healthCurrentPlayer += amounHealing;
            if (gameManager.healthCurrentPlayer > gameManager.maxHealthPlayer)
            {
                gameManager.healthCurrentPlayer = gameManager.maxHealthPlayer;
            }
        }

    }

    void DamagePlayer() // Damage by one
    {
        gameManager.healthCurrentPlayer--;
        if (gameManager.healthCurrentPlayer <= 0)
        {
            gameManager.healthCurrentPlayer = 0;
            RestLife();
        }
    }

    public void DamagePlayer(float amount) // Damage by amount
    {
        gameManager.healthCurrentPlayer -= amount;
        if (gameManager.healthCurrentPlayer <= 0)
        {
            gameManager.healthCurrentPlayer = 0;
            RestLife();
        }
    }

    void RestLife()
    {
        gameManager.lifesPlayer--;

        if (gameManager.lifesPlayer <= 0)
        { 
            gameManager.lifesPlayer = 0;
        }
    }

    void SaveTransform()
    {
        savedPosition = transform.position;
        savedRotation = transform.rotation;
        savedScale = transform.localScale;
        //Debug.Log("Entra SaveTransform");
    }

    void Respawn()
    {
         transform.position = savedPosition;
         transform.rotation = savedRotation;
         transform.localScale = savedScale;
        // Remove force RigibBody
        rb.velocity = Vector3.zero;
        //Debug.Log("Entra Respawn");


    }
}