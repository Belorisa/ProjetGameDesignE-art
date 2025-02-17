using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoomrang :Tower
{
    //special stats
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        StartForEveryOne(this, new BoomRangTower(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
