using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMovPlayer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject goalDestination;
    public Camera cameraControl;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;
    private int collectedCoins;
    private int collectedMaps;

    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
        navMeshAgent.destination = goalDestination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //navMeshAgent.destination = goalDestination.transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraControl.ScreenPointToRay(Input.mousePosition), out hit))
            {
                navMeshAgent.destination = hit.point;
            }
        }

        if (transform.position.y < -10)
        {
            Respawn();
        }

    }

    public void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;
    }


}
