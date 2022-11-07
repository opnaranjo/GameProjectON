using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCanvas : MonoBehaviour
{
    public static SingletonCanvas instance;

    private void Awake()
    {
        if (SingletonCanvas.instance == null)
        {
            SingletonCanvas.instance = this;
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
