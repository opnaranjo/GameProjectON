using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzador : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject granadaPrefab;
    public float range = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrande();
        }
    }

    void ThrowGrande()
    {
        GameObject go = Instantiate(granadaPrefab, spawnPoint.position,spawnPoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * range,ForceMode.Impulse);
    }
}
