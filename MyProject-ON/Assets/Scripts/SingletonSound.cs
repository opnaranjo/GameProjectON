using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSound: MonoBehaviour
{
    public static SingletonSound instance;

    private void Awake()
    {
        if (SingletonSound.instance == null)
        {
            SingletonSound.instance = this;
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
