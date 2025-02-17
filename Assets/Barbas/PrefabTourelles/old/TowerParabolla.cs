using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParabolla : Tower
{
    //special stats
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ParabolaTower(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
