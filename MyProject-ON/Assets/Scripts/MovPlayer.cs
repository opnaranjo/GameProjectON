using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float[] speedMov; // Caminar: 2.0f - Sneaking: 1.0f - Correr: 5.0f ;
    public float[] speedRot; // Caminar 100.0f - Sneaking: 50.0f - Correr: 200.0f;

    public Rigidbody rb; // Para controlar la animación de salto
    public float[] jumpingForce;
    public bool jumped;

    private float x, y;
    private Animator anim;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;
    private int movIndex;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        jumped = false;
        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
        movIndex = 1;
}

    // Update is called once per frame
    void Update()
    {

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

        // Vuelve a posición inicial si cae del escenario
        if (transform.position.y < -10)
        {
            Respawn();
        }

    }

    public void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;
    }

    // Estandariza el tiempo de transición entre cada cuadro no importa el equipo en el que corra el juego
    void FixedUpdate()
    {
        transform.Rotate(0, x * Time.deltaTime * speedRot[movIndex], 0);
        transform.Translate(0, 0, y * Time.deltaTime * speedMov[movIndex]);
    }

    public void playerFalling()
    {
        anim.SetBool("touchFloor", false);
        anim.SetBool("jumping", false);
    }
}
