using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysChangeSprite : EnemyWayPoint2
{
    public Animator animator;

    //public Sprite spriteOnX;

    public Sprite spriteUp;
    public Sprite spriteDown;
    
    new void Start()
    {
        StartForEvryOne();

        waypoints = gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints;
        speed = myStats.speed;
        lastWaypointSwitchTime = Time.time;

        GetComponentInChildren<HealthBar>().maxHealth = myStats.hp; // Setup les point de vie dans le script Health
        GetComponentInChildren<HealthBar>().currentHealth = myStats.hp;
        
        SwitchDirection();
    }

    public override void SwitchDirection() //V2 des rotation des enemy
    {
        Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;

        float diff = newStartPosition.x - newEndPosition.x;

        if (newStartPosition.x > newEndPosition.x)
        {
            animator.enabled = true;
            
            //GetComponent<SpriteRenderer>().sprite = spriteOnX;
            
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (newStartPosition.x < newEndPosition.x)
        {
            animator.enabled = true;
            
            //GetComponent<SpriteRenderer>().sprite = spriteOnX;
            
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (newStartPosition.y < newEndPosition.y)
        {
            animator.enabled = false;
            
            GetComponent<SpriteRenderer>().sprite = spriteUp;
        }
        
        if (newStartPosition.y > newEndPosition.y)
        {
            animator.enabled = false;
            
            GetComponent<SpriteRenderer>().sprite = spriteDown;
        }
    }
}
