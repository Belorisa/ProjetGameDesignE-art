using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLego : Tower
{
    //special stats

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new LegoTowerMainState(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
