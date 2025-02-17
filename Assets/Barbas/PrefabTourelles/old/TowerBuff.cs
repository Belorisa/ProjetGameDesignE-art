using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuff : Tower
{
    //special stats

    public int buffEffect;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new BuffTower(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}
