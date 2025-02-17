using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeath : MonoBehaviour
{
    public AudioClip deathSound;

    public float volume;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDestroy()
    {
    AudioSource.PlayClipAtPoint(deathSound, new Vector3(transform.position.x,transform.position.y,-8), volume);
    }
}
