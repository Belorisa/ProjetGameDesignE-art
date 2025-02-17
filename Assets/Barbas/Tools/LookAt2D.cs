using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    public Vector2 pos;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        float angle = 0;
         
        Vector3 relative = transform.InverseTransformPoint(pos);
        angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
        transform.Rotate(0,0, -angle);
    }
}
