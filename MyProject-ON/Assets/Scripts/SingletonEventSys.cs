using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventSys : MonoBehaviour
{
    public static SingletonEventSys instance;

    private void Awake()
    {
        if (SingletonEventSys.instance == null)
        {
            SingletonEventSys.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
