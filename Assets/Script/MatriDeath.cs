using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatriDeath : MonoBehaviour
{
    
    public GameObject matri;

    private GameObject matrio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
       matrio = Instantiate(matri,transform.position,transform.rotation);
       matrio.GetComponent<EnemyWayPoint2>().currentWaypoint = GetComponent<EnemyWayPoint2>().currentWaypoint;
       matrio.GetComponent<EnemyWayPoint2>().currentTimeOnPath = GetComponent<EnemyWayPoint2>().currentTimeOnPath;

    }
}
