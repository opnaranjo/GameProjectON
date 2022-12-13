using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PlayerInteraction : MonoBehaviour
{
    public float timer = 0f;
    public float limitTimer = 3f;
    public float limitFloor = -10;

    public float blinkDelay = 1.25f;
    //public float blinkWait = 3.5f;
    //public float waitHealing = 4.5f;
    //public float waitRespawn = 3.5f;

    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private Vector3 savedScale;
    private GameManager gameManager;
    private Rigidbody rb;
    private EventManager eventManager;
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
        eventManager = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // Respawn player if fall 
        if (transform.position.y < limitFloor)
        {
            DamagePlayer(gameManager.maxHealthPlayer);
            Respawn();
            HealingPlayer(gameManager.maxHealthPlayer);
        }


    }
    
    void OnTriggerEnter(Collider col)
    {
        if (!gameManager.isDead)
        {
            // Nombre del GameObject
            //Debug.Log("GameObject: " + col.name);

            if (col.transform.gameObject.CompareTag("Coin"))
            {

                Destroy(col.transform.gameObject);
                gameManager.collectedCoins++;
            }

            if (col.transform.gameObject.CompareTag("Map"))
            {

                Destroy(col.transform.gameObject);
                gameManager.collectedMaps++;
            }

            if (col.transform.gameObject.CompareTag("Trap"))
            {
                DamagePlayer();
            }

            if (col.transform.gameObject.CompareTag("Enemy"))
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
                    SceneManager.LoadScene(2);
                }

                if (col.transform.gameObject.tag == "Portal2")
                {
                    SceneManager.LoadScene(1);
                }

                timer = 0;
            }
        }
    }

    public void Blink()
    {
        StartCoroutine(BlinkPlayer(blinkDelay));
        //StartCoroutine(Function(3.0f));
    }

    public IEnumerator BlinkPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var rend in renderers)
        {
            rend.enabled = false;

        }
        /*
        // tiempo espera
        timer += Time.deltaTime;
        if (timer >= limitTimer)
        {

            timer = 0;
        }
        */
        yield return new WaitForSeconds(delay);

        foreach (var rend in renderers)
        {
            rend.enabled = true;

        }
        //yield return new WaitForSeconds(delay);

    } 
        
    public void RestoreAllHealth()
    {
        gameManager.healthPlayer = gameManager.maxHealthPlayer;
    }

    public void Hello()
    {
        Debug.Log("Hello method PlayerInteraction.cs");
    }

    public void Bye()
    {
        Debug.Log("Bye method PlayerInteraction.cs");
    }

    public void Other()
    {
        Debug.Log("Other method PlayerInteraction.cs");
    }

    void HealingPlayer() // Healing by one
    {
        if (gameManager.lifesPlayer > 0)
        {
            gameManager.healthPlayer++;
            if (gameManager.healthPlayer > gameManager.maxHealthPlayer)
            {
                gameManager.healthPlayer = gameManager.maxHealthPlayer;
            }
        }

    }

    void HealingPlayer(float amounHealing)
    {
        if (gameManager.lifesPlayer > 0)
        {
            gameManager.healthPlayer += amounHealing;
            if (gameManager.healthPlayer > gameManager.maxHealthPlayer)
            {
                gameManager.healthPlayer = gameManager.maxHealthPlayer;
            }
        }

    }

    void DamagePlayer() // Damage by one
    {
        gameManager.healthPlayer--;
        if (gameManager.healthPlayer <= 0)
        {
            gameManager.healthPlayer = 0;
            RestLife();
        }
    }

    public void DamagePlayer(float amount) // Damage by amount
    {
        gameManager.healthPlayer -= amount;
        if (gameManager.healthPlayer <= 0)
        {
            gameManager.healthPlayer = 0;
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