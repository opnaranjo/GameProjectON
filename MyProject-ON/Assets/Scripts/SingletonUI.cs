using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonUI : MonoBehaviour
{
    public static SingletonUI instance;

    private void Awake()
    {
        if (SingletonUI.instance == null)
        {
            SingletonUI.instance = this;
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
