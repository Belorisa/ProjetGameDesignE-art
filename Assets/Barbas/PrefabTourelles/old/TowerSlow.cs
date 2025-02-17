using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlow : Tower
{
    //special stats
    public TowerSlowSpecialStats[] speciaStatesPerLevel;

    public TowerSlowSpecialStats mySpecialStats;
        
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        //mainState = new SlowTower(this);
        
        //SetState(new BuildingState(this, new SlowTower(this)));

        StartForEveryOne(this, new SlowTower(this));
    }
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }
}

[System.Serializable]
public class TowerSlowSpecialStats
{
    public float slowTimer;

    public float slow;
}
