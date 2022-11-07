using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionPlayer : MonoBehaviour
{
    public int continuePlaying = 3;
    public int life = 2;
    public int collectedCoins = 0;
    public int collectedMaps = 0;
    public float timer = 0f;
    public float limitTimer = 2f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            GameManager.PausedSound();
        }

        if (Input.GetKey(KeyCode.U))
        {
            GameManager.UnPausedSound();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // Nombre del GameObject
        //Debug.Log("GameObject: " + col.name);

        if (col.transform.gameObject.tag == "Coin")
        {

            Destroy(col.transform.gameObject);
            collectedCoins++;
        }

        if (col.transform.gameObject.tag == "Map")
        {

            Destroy(col.transform.gameObject);
            collectedMaps++;
        }

        if (col.transform.gameObject.tag == "Spikes")
        {

            Respawn();
        }

        if (col.transform.gameObject.tag == "Enemy")
        {

            Respawn();
        }

    }

    void OnTriggerStay(Collider col)
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

    void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;
    }
}