using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private string enemyName;
    [SerializeField] protected private float moveSpeed;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;

    protected private Transform target;//The Target is our player
    [SerializeField] protected private float distance;

    //public Image hpImage;//Red Health Bar
    //public Image hpEffectImage;//White Health Bar Hurting Effect

    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private Slider slider;

    private void Start()
    {
        healthPoint = maxHealthPoint;
        slider.value = CalculateHealth();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
    }

    protected virtual void Introduction()
    {
        Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }


    protected virtual void Attack()
    {
        Debug.Log(enemyName + " is Attacking");
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    float CalculateHealth()
    {
        return healthPoint / maxHealthPoint;
    }

    /*
    protected virtual void DisplayHpBar()
    {
        hpImage.fillAmount = healthPoint / maxHealthPoint;

        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= 0.005f;//Delay Effect
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;//STOP continue Decreasing 
        }
    }
    */

}