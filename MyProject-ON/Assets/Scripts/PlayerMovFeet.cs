using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovFeet : MonoBehaviour
{
    public PlayerMov playerMov;
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
        playerMov.jumped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerMov.jumped = false;
    }
}
