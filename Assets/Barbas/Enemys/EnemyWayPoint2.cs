using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoint2 : Ennemis
{
    [HideInInspector]
    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    protected float lastWaypointSwitchTime;
    protected float speed;

    public float currentTimeOnPath;
    
    new void Start()
    {
        StartForEvryOne();

        waypoints = gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints;
        speed = myStats.speed;
        lastWaypointSwitchTime = Time.time;

        GetComponentInChildren<HealthBar>().maxHealth = myStats.hp; // Setup les point de vie dans le script Health
        GetComponentInChildren<HealthBar>().currentHealth = myStats.hp;
    }
    
    new void Update()
    { 
        currentState.Tick();
        
        GetComponentInChildren<HealthBar>().currentHealth = myStats.hp; //Mets a jour les point dans le script Health qui lui est liée
        
        if (myStats.hp <= 0)
        {
            Death();
        }

        for (int i = debuffs.Count - 1; i >= 0; i--)
        {
            debuffs[i].Tick();
        }

        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
// 2 
        float pathLength = Vector2.Distance(startPosition, endPosition);

        float totalTimeForPath = pathLength / speed; // * gears.GetComponent<Gears>().globalActionSpeedScale;

        currentTimeOnPath += Time.deltaTime * myStats.speed; // * gears.GetComponent<Gears>().globalActionSpeedScale;  //Calcul la durée entre 2 point en fonction de la vitesse de l'enemy
        //gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        
        transform.position = new Vector3(Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath).x, 
            Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath).y, transform.position.z);
// 3 
        if (gameObject.transform.position == endPosition || new Vector2(transform.position.x, transform.position.y) == new Vector2(endPosition.x, endPosition.y))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                // 3.a 
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                
                SwitchDirection();
            }
            else
            {
                // 3.b 
                gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().hp -=
                    myStats.hpValue;
                
                //faire gagner les golds?
                
                if (gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().hp <= 0) //pas mettre dans l'update
                {
                    Time.timeScale = 0;
                    
                    gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().gameOverPanel.SetActive(true);
            
                    Debug.Log("Loose");
                }
                
                Destroy(gameObject);
            }

            currentTimeOnPath = 0;
        }
    }

    public virtual void SwitchDirection() //V2 des rotation des enemy
    {
        Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;

        float diff = newStartPosition.x - newEndPosition.x;

        if (newStartPosition.x > newEndPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (newStartPosition.x < newEndPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (diff < 1)
        {
            GetComponent<SpriteRenderer>().flipX = false; 
        }
    }
    
    private void RotateIntoMoveDirection()
    {
        //1 
        Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
        //3
        
        GetComponent<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    } //V1 des rotation des enemy
}
