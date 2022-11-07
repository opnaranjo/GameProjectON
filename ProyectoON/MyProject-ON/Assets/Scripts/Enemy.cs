using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private enum EnemyType
    {
        EnemyOne,
        EnemyTwo,
    };

    [SerializeField] private EnemyType tipoEnemigo;

    public Transform posJugador;
    public GameObject happyFace;
    public GameObject ungryFace;
    public NavMeshAgent agent;


    public float speedMov = 2f;
    public float speedLook = 2f;
    public float maxDistance = 5f;
    /*
    private float lifeEnemy = 1f;
    private float attackDamage = 1f;
    */
    private bool flagMovEnemy = false;
    private bool flagLookEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyBehaviour(tipoEnemigo);
    }

    void SetEnemyBehaviour(EnemyType tipoEnemigo)
    {
        switch (tipoEnemigo)
        {

            case EnemyType.EnemyOne:
                flagLookEnemy = true;
                flagMovEnemy = false;
                break;
            case EnemyType.EnemyTwo:
                flagLookEnemy = true;
                flagMovEnemy = true;
                break;
            default:
                flagLookEnemy = false;
                flagMovEnemy = false;
                break;
        }
    }

    /*
    void TrabajandoConVectores()
    {
        Vector3 vectorA = new Vector3(2, 2, 2);
        Vector3 vectorB = new Vector3(2, 3, 1);
        Vector3 res = vectorA + vectorB;

        Debug.Log("Resultado: " + res);

        vectorA = Vector3.left;

        Debug.Log("El largo del vector es: " + vectorA.magnitude);
        transform.position += new Vector3(1, 3, 0);

        // Normalizar para detectar una dirección
        // (Se queda en un rango específico)
        res = res.normalized;
    }
    */


    // Update is called once per frame
    void Update()
    {
        ChequaerDistancia();
        //LookAtPleyerQuaternion();
        //SeguirDistancia();
        /*
        if (flagMovEnemy)
        {
            SeguirAlJugadorLerp();
        }
        */
        if (flagLookEnemy)
        {
            LookAtPlayerLerp();
        }
        if (flagMovEnemy)
        {
            SetearDestino();
            SeguirAlJugadorLerp();
        }
        

    }

    
    void ChequaerDistancia()
    {
        float dist = Vector3.Distance(posJugador.position, transform.position);

        //Debug.Log("Distancia:" + dist);

        if (dist <= maxDistance)
        {
            happyFace.SetActive(false);
            ungryFace.SetActive(true);
            speedMov = 0;
        }
        else
        {
            happyFace.SetActive(true);
            ungryFace.SetActive(false);
            speedMov = 2f;

        }

       
    }
    

    /*
    void LookAtPleyerQuaternion()
    {
        Quaternion rot = Quaternion.LookRotation(posJugador.position - transform.position);
        transform.rotation = rot;
    }
    */

    /*
    void LookAtPlayer()
    {
        transform.LookAt(posJugador);
    }
    */

    
    void LookAtPlayerLerp()
    {
        Quaternion nuevaRotacion = Quaternion.LookRotation(posJugador.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, nuevaRotacion, speedLook * Time.deltaTime);
    }
    

    /*
    void SeguirDistancia()
    {
        transform.position = Vector3.MoveTowards(transform.position, posJugador.position, speed * Time.deltaTime);
    }
    */

    
    void SeguirAlJugadorLerp()
    {
        transform.position = Vector3.Lerp(transform.position, posJugador.position, speedMov * Time.deltaTime);
    }
    

    void SetearDestino()
    {
        agent.SetDestination(posJugador.position);
    }
}
