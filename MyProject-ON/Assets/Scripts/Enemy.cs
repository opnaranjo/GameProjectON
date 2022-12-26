using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private string enemyName;
    [SerializeField] private float healthPoint;
    [SerializeField] private float maxHealthPoint;
    [SerializeField] protected float damage;

    //protected private Transform target;//The Target is our player
    //[SerializeField] protected private float distance;

    //public Image hpImage;//Red Health Bar
    //public Image hpEffectImage;//White Health Bar Hurting Effect

    [SerializeField] private GameObject healthBarUI;
    [SerializeField] protected Slider slider;
    public GameManager gameManager;

    [Header("---------------------------")]
    [Header("Enemy Movement")]
    [SerializeField] protected private float speedLook;
    //[SerializeField] protected private float moveSpeed;
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    public GameObject[] destinations; // Usa un array de destinos para poder asignar tantos destinos como desees (excepto el jugador)
    public float distanceToFollowPlayer = 5f;

    Vector3 currentTarget; // Almacena el objetivo actual al que se dirige (incluirá al jugador)
    int currentDestination = 0; // Controla el destino actual al que se dirige (del array de destinos)

    private void Start()
    {
        healthPoint = maxHealthPoint;
        slider.value = CalculateHealth();

        gameManager = FindObjectOfType<GameManager>();
        currentTarget = destinations[currentDestination].transform.position;

        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //sp = GetComponent<SpriteRenderer>();

        Introduction();
    }

    private void Update()
    {

        slider.value = CalculateHealth();

        if (healthPoint < maxHealthPoint)
        {
            healthBarUI.SetActive(true);
        }

        if (healthPoint <= 0)
        {
            //  Death();
        }

        if (healthPoint > maxHealthPoint)
        {
            healthPoint = maxHealthPoint;
        }



        // -------------------------

        //DisplayHpBar();//MARKER REMOVE LATER if you have one Hurt Method (OPTIONAL)

        if (healthPoint <= 0)
        {
            Death();
        }
         
        Attack();

        //MARKER TEST THE HEALTH BAR
        if (Input.GetKeyDown(KeyCode.X))
        {
            healthPoint -= 10;
        }

        
    }

    private void FixedUpdate()
    {
        Move();
        LookAtPlayerLerp(this.gameObject);
    }

    protected virtual void Introduction()
    {
        //Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
        Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint);
    }

    protected virtual void Move()
    {
        if (Vector3.Distance(destinations[currentDestination].transform.position, transform.position) < 1) // Controla cuando alcanza el destino actual (no es recomendable poner "igual a 0")
        {
            if (currentDestination == destinations.Length - 1) // Si el destino actual es el último del array ...
            {
                currentDestination = 0; // ... volverá a empezar de nuevo
            }
            else 
            {
                currentDestination++; // ... continuará con el siguiente destino
            }
        }

        if ((!gameManager.isDead) && (Vector3.Distance(player.transform.position, transform.position) < distanceToFollowPlayer)) // Si el jugador está dentro de la distancia especificada para empezar a seguirlo ...
        {
            currentTarget = player.transform.position; // ... asigna como objetivo actual al jugador
        }
        else 
        {
            currentTarget = destinations[currentDestination].transform.position; // ... continúa con el destino que le corresponde (también controla que el jugador consiga escapar si corre más que el enemigo)
        }

        navMeshAgent.destination = currentTarget; // Asigna el objetivo al que debe ir, ya sea destino o jugador
    }

    
    void LookAtPlayerLerp(GameObject gameObject)
    {
        Quaternion nuevaRotacion = Quaternion.LookRotation(player.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, nuevaRotacion, speedLook * Time.deltaTime);
    }
    

    protected virtual void Attack()
    {
        //gameManager.DamagePlayer(damage);
        Debug.Log(enemyName + " is Attacking");
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    protected float CalculateHealth()
    {
        return healthPoint / maxHealthPoint;
    }

    

}