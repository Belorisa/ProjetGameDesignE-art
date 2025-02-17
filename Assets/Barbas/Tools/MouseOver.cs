using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public bool mouseOver;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseOver() //need collider
    {
        mouseOver = true;
    }
    
    void OnMouseExit()
    {
        mouseOver = false;
    }
}
