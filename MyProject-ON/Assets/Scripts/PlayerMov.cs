using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float[] speedMov; // Walking: 2.0f - Sneaking: 1.0f - Running: 5.0f ;
    public float[] speedRot; // Walking 100.0f - Sneaking: 50.0f - Running: 200.0f;


    public float[] jumpingForce;
    public bool jumped;

    private Rigidbody rb; // Para controlar la animación de salto
    private float x, y;
    private Animator anim;
    //private Vector3 startPosition;
    //private Quaternion startRotation;
    //private Vector3 startScale;
    private int movIndex;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        //gameManager = FindObjectOfType<GameManager>();
        // Get Animator
        anim = GetComponent<Animator>();
        //RigidBody Player
        rb = GetComponent<Rigidbody>();
        jumped = false;

        //startPosition = transform.position;
        //startRotation = transform.rotation;
        //startScale = transform.localScale;
        movIndex = 1;
}

    // Update is called once per frame
    void Update()
    {

        if (gameManager == null)
        { 
            gameManager = FindObjectOfType<GameManager>();
        }

        if (!gameManager.isDead)
        {
            // Reset Death
            anim.SetBool("dying", false);

            // Movimiento sigiloso
            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                anim.SetBool("sneaking", true);
                movIndex = 0;
            }
            if (Input.GetKeyUp(KeyCode.RightAlt))
            {
                anim.SetBool("sneaking", false);
                movIndex = 1;
            }
            // Movimiento corriendo
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                anim.SetBool("running", true);
                movIndex = 2;
            }
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                anim.SetBool("running", false);
                movIndex = 1;
            }

            // Movimiento Salto
            if (jumped)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("jumping", true);
                    rb.AddForce(new Vector3(0, jumpingForce[movIndex],0),ForceMode.Impulse);
                }

                anim.SetBool("touchFloor", true);

            }
            else
            {
                playerFalling();
            }
        
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            anim.SetFloat("velX", x);
            anim.SetFloat("velY", y);
        }
        else
        {
            //anim.SetTrigger("death");
            anim.SetBool("dying", true);
        }

    }

    // Estandariza el tiempo de transición entre cada cuadro no importa el equipo en el que corra el juego
    void FixedUpdate()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (!gameManager.isDead)
        {
            transform.Rotate(0, x * Time.deltaTime * speedRot[movIndex], 0);
            transform.Translate(0, 0, y * Time.deltaTime * speedMov[movIndex]);
        }
    }

    public void playerFalling()
    {
        anim.SetBool("touchFloor", false);
        anim.SetBool("jumping", false);
    }
}
