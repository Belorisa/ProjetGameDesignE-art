using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject gears;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }
    
    void FixedUpdate()
    {
        if (gears.GetComponent<Gears>().playerI != null)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(gears.GetComponent<Gears>().playerI.transform.position.x, gears.GetComponent<Gears>().
                playerI.transform.position.y, transform.position.z), 0.125f);   //Smooth Camera
            
            transform.position = smoothedPosition;
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y), 
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)); //Camera Bounds
    }
}
