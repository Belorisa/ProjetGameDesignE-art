using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovments : StateTower
{
    public BulletV2 me;

    public BulletMovments(TowerV2 towerV2) : base(towerV2)
    {
        
    }
    
    public BulletMovments ShallowCopy()
    {
        return (BulletMovments) MemberwiseClone();
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
}
