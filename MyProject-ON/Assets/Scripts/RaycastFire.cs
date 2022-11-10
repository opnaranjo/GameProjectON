using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFire : MonoBehaviour
{

    public GameObject posPlayer;
    private float range = 100f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

    }
}
