using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class ScritpJugador : MonoBehaviour
{
    private float movX;
    private float movY;

    public float speedMov = 5f;
    public Rigidbody rb;

    public Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        MovimientoJugador();
    }

    void MovimientoJugador()
    {

        pos = transform.position;
        // Variables para capturar movimiento con rotacion
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        //Vector3 inputPlayer = movX + movZ;
        Vector3 inputPlayer = new Vector3(movX, 0, movY);

        // Realiza el movimiento
        rb.AddForce(inputPlayer * speedMov);
        transform.Translate(inputPlayer * speedMov * Time.deltaTime);
        //rb.AddForceAtPosition(inputPlayer * speedMov, pos);
    }

}
