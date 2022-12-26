using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class PlayerInteraction : MonoBehaviour
{
    public float timer = 0f;
    public float limitTimer = 3f;
    public float limitFloor = -10;

    //public float blinkWait = 3.5f;
    //public float waitHealing = 4.5f;
    //public float waitRespawn = 3.5f;

    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private Vector3 savedScale;
    private GameManager gameManager;
    private Rigidbody rb;
    private GameObject goPlayer;
    private Renderer[] renderers;
    private float blinkDelay = 0.5f;
    //private EventManager eventManager;


    // Start is called before the first frame update
    void Start()
    {
        SaveTransform();
        //Capture GameManager
        gameManager = FindObjectOfType<GameManager>();
        //RigidBody Player
        rb = GetComponent<Rigidbody>();

        //gameManager.isDead = false;

        /*
        goPlayer = GameObject.FindWithTag("Player");

        if (goPlayer != null)
        {

            //PlayerInteraction playerScript = goPlayer.GetComponent<PlayerInteraction>();
            renderers = goPlayer.GetComponentsInChildren<Renderer>();

            //if (playerScript != null)
            //{

            // Do relevant stuff here

            //}
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

        // Respawn player if fall 
        if (transform.position.y < limitFloor)
        {
            gameManager.DamagePlayer(gameManager.maxHealthPlayer);
            Respawn();
            gameManager.HealingPlayer(gameManager.maxHealthPlayer);
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
                gameManager.DamagePlayer(1);
            }

            if (col.transform.gameObject.CompareTag("Enemy"))
            {

                gameManager.DamagePlayer(1);
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

    public void Blink()
    {

        //MonoBehaviour camMono = Camera.main.GetComponent<MonoBehaviour>();

        goPlayer = GameObject.FindWithTag("Player");

        if (goPlayer != null)
        {

            PlayerInteraction playerScript = goPlayer.GetComponent<PlayerInteraction>();
            //renderers = goPlayer.GetComponentsInChildren<Renderer>();

            if (playerScript != null)
            {
                //Debug.Log("Entra a playerScript::: " + playerScript);
                playerScript.StartCoroutine(BlinkObject(blinkDelay));
                //goPlayer.SetActive(true);

            }
        }
        

        
        //Debug.Log("Entra a Blink" + goPlayer);
        //StartCoroutine(Function(3.0f));
    }

    public IEnumerator BlinkObject(float delay)
    {


        //goPlayer = GameObject.FindWithTag("Player");

        //Debug.Log("Entra a IEnumerator Blink" + goPlayer);

        if (goPlayer != null)
        {
            renderers = goPlayer.GetComponentsInChildren<Renderer>();

            if (renderers.Length != 0)
            {

                yield return new WaitForSeconds(delay);

                foreach (var rend in renderers)
                {
                    rend.enabled = false;
                }

                yield return new WaitForSeconds(delay);

                foreach (var rend in renderers)
                {
                    rend.enabled = true;
                }

            }
        }

    }
}