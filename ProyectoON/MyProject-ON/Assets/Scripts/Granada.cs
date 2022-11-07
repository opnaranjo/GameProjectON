using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{

    public GameObject efectoExplosion;
    public float delay = 3f;
    public float radius = 20f;
    public float exploxionForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ExplosionGranada", delay);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplosionGranada()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider near in cols)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(exploxionForce,transform.position,radius,1f,ForceMode.Impulse); // 1f Que tan alto van a estar los objetos
            }

        }

        Instantiate(efectoExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
