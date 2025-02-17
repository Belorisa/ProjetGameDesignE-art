using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    
    public AudioClip shootSound;

    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(shootSound, new Vector3(transform.position.x,transform.position.y,-8), volume);
    }
}
