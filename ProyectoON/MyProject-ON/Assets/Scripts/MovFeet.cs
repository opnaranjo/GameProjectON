using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFeet : MonoBehaviour
{
    public MovPlayer movPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        movPlayer.jumped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        movPlayer.jumped = false;
    }
}
