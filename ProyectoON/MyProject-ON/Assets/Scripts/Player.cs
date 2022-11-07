using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{

    public GameObject prefabBala;
    public Transform spawnPos;
    public float timer = 0f;
    public float limitTimer = 2f;
    public float speedMov = 5f;
    public float speedRot = 200f;
    //public Animator animacion;
    public float acceleration = 2f;
    public float deceleration = 0.2f;

    Vector3 posInicial;
    Quaternion rotInicial;
    Vector3 scaleInicial;

    //float velAnimacion = 0.0f;
    float rndX;
    float rndY;
    float rndZ;


    private int monedasObtenidas;
    private bool smallflag;
    

    // Start is called before the first frame update
    void Start()
    {
        smallflag = false;
        posInicial = transform.position;
        rotInicial = transform.rotation;
        scaleInicial = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {


        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0,0,0.1f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -0.1f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-0.1f, 0, 0));
        }

        */

        //TemporizadorDisparo();

        MovimientoJugador();

        // Movimiento Giro
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -0.5f, 0) * speedRot * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 0.5f, 0) * speedRot * Time.deltaTime);
        }

        /*
        // Movimiento Animacion
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animacion.SetBool("estaCorriendo", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animacion.SetBool("estaCorriendo", false);
        }
        */

        //bool keyForward = Input.GetKey("w");
        bool keySpeed = Input.GetKey("right shift");

        /*
        if(keySpeed && velAnimacion < 1.0f)
        {
            velAnimacion += Time.deltaTime * acceleration;
        }

        if (!keySpeed && velAnimacion > 0.0f)
        {
            velAnimacion -= Time.deltaTime * deceleration;
        }
        */

        //animacion.SetFloat("Velocidad", velAnimacion);

        // Reinicio posición cuando cae
        if (transform.position.y <= -10)
        {
            Respawn();
        }

        // Disparo con ratón
        if (Input.GetMouseButton(0))
        {
            Disparo();
        }

        //MouseTest();

    }  /// FIN UPDATE

    //////////// Métodos //////////////////


    void Respawn()
    {
        transform.position = posInicial;
        transform.rotation = rotInicial;
    }


    // OnTriggerStay: Mientras los objetos permanecen en contacto (Teleport, Recupera vida, etc.)
    // OnTriggerEnter: Cuando una caja colisonadora entra en contacto con otra caja (Atraviesa objetos)
    // OnTriggerExit: Cuando una dejan en contacto con otra caja colisionadora
    // OnCollisionEnter: No atraviesa (Peleas, etc.) 
    void OnTriggerEnter(Collider col)
    {
        // Nombre del GameObject
        Debug.Log("GameObject: "+ col.name);

        if (col.transform.gameObject.tag == "Moneda")
        {

            Destroy(col.transform.gameObject);
            monedasObtenidas++;
        }

        if (col.transform.gameObject.tag == "Spikes")
        {

            Respawn();
        }
    }

    void OnTriggerStay(Collider col)
    {
        
        rndX = Random.Range(250, 330);
        rndY = Random.Range(10, 20);
        rndZ = Random.Range(20, 60);
        
        timer += Time.deltaTime;
        if (timer >= limitTimer)
        {
            if (col.transform.gameObject.tag == "Teleport1")
            {
                transform.position = new Vector3(rndX, rndY, rndZ);
                transform.Rotate(new Vector3(0, 180f, 0));
            }

            if (col.transform.gameObject.tag == "Teleport2")
            {
                transform.position = posInicial;
                transform.rotation = rotInicial;
            }

            if (col.transform.gameObject.tag == "Portal")
            {
                if (smallflag == false)
                {
                    transform.localScale = transform.localScale / 1.5f;
                    smallflag = true;
                }
                else
                {
                    transform.localScale = scaleInicial;
                    smallflag = false;
                }
            }
            timer = 0;
        }

    }

    void MovimientoJugador()
    {
        /*
        // Variables para capturar movimiento
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector3 inputPlayer = new Vector3(movX, 0, movZ); // Para captar el movimiento en una sola variable 
        */

        // Variables para capturar movimiento con rotacion
        Vector3 movX = Input.GetAxis("Horizontal") * transform.right;
        Vector3 movZ = Input.GetAxis("Vertical") * transform.forward;

        Vector3 inputPlayer = movX + movZ;

        // Realiza el movimiento 
        transform.Translate(inputPlayer * speedMov * Time.deltaTime);

        /*
        // Activa la animacion
        if (inputPlayer == Vector3.zero)
        {
            animacion.SetBool("estaCorriendo", false);
        }
        else
        {
            animacion.SetBool("estaCorriendo", true);
        }
        */

    }

    void Disparo()
    {
        Instantiate(prefabBala, spawnPos.position, spawnPos.rotation);
    }

    /*
    void TemporizadorDisparo()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            timer = limitTimer;
            Disparo();
        }
    }
    */


    /*
    void MouseTest()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(prefabBala, worldPosition, Quaternion.identity);
        }
    }
    */

}
