using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTowerLevelUp : StateTower
{
    protected Tower myTower;
    public StateTowerLevelUp(Tower tower) : base(tower)
    {
        myTower = tower;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
    }
    public override void Tick()
    {
        
    }
    
    public override void OnStateExit()
    {
    }
    
    public virtual void OnLevelUp(int i)
    {}
}
