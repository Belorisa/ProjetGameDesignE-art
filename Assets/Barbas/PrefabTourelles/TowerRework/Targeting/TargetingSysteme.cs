using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSysteme : StateTower
{
    public TowerV2 towerV2;
    
    public GameObject target;
    
    public TargetingSysteme(TowerV2 towerV2) : base(towerV2)
    {
        this.towerV2 = towerV2;
    }
    
    public TargetingSysteme ShallowCopy()
    {
        return (TargetingSysteme) MemberwiseClone();
    }

    public override void OnStateEnter()
    { 
        //Debug.Log("");
    }
    public override void Tick()
    {
       //target
    }
    
    public override void OnStateExit()
    {
    }
}
