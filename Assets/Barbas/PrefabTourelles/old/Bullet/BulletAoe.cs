using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAoe : Bullet
{
    public float area;

    public GameObject explosion;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        SetState(myStats.behaviour);
        
        myStats.effect?.OnStateEnter();
    }
    
    new void Update()
    {
        currentState.Tick();
        
        myStats.effect?.Tick(); //? = null propagation = check si c null
        
        if (transform.position.x >= point.x || transform.position.x <= point2.x || transform.position.y >= point.y || transform.position.y <= point2.y) //détruire si trop loing
        {
            DestroyObject(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Area bullet collided with something");
    }
}
