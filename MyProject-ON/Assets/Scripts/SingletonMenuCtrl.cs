using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventMgr : MonoBehaviour
{
    public static SingletonEventMgr instance;

    private void Awake()
    {
        if (SingletonEventMgr.instance == null)
        {
            SingletonEventMgr.instance = this;
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
