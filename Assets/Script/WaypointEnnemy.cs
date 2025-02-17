using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEnnemy : MonoBehaviour
{

    [HideInInspector]
    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1; /** GetComponent<EnemyStat>().speed; **/
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
// 2 
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
// 3 
        if (gameObject.transform.position == endPosition)
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                // 3.a 
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else
            {
                // 3.b 
                Destroy(gameObject);

                
                // TODO: deduct health
            }
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
    }
}


