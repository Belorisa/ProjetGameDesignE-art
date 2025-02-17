using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrap : Tower
{
    //special stats

    public float trapDuration;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new TrapTowerBeh(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
