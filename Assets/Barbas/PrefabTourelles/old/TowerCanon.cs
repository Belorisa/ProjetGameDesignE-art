using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCanon : Tower
{
    //special stats

    public float area;

    public float[] areaPerlevel;

    public GameObject explosion;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new CanonTower(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
