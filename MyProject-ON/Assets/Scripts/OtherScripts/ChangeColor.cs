using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // EventManager.ejemploEvento += ChangeColorObj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ChangeColorObj()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }
    private void OnDisable()
    {
       // EventManager.ejemploEvento -= ChangeColorObj; // Si el objeto es eliminado
    }
}
