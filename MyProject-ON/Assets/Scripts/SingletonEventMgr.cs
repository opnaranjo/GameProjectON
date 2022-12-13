using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMenuCtrl : MonoBehaviour
{
    public static SingletonMenuCtrl instance;

    private void Awake()
    {
        if (SingletonMenuCtrl.instance == null)
        {
            SingletonMenuCtrl.instance = this;
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
