using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static UnityEditor.SceneView;

public class CamerasControl : MonoBehaviour
{

    public List<GameObject> cameras = new List<GameObject>();
    public CinemachineFreeLook cameraLook; // Para activar con botón de ratón

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCamera();
    }

    void ChangeCamera()
    {

        
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCamera();
        }
        
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            cameraLook.m_YAxis.m_InputAxisName = "Mouse Y";
            cameraLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else
        {
            cameraLook.m_YAxis.m_InputAxisName = "";
            cameraLook.m_XAxis.m_InputAxisName = "";
        }
    }

    void ToggleCamera()
    {
        int index = 0;
        int limit = cameras.Count;

        foreach (var camera in cameras)
        {

            if (camera.activeInHierarchy)
            {
                //Debug.Log("Index Activo: " + cameras.IndexOf(camera) + "Limite" + cameras.Count);
                index = cameras.IndexOf(camera);

                if (index == limit-1)
                {
                    index = 0;
                }
                else
                { 
                    index++;
                }
            }
            camera.SetActive(false);
        }
        cameras[index].SetActive(true);

    }
    
    
}
