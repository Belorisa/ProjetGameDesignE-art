using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForwardMovments : BulletMovments
{
    public Vector2 targetedPos;
    public BulletForwardMovments(TowerV2 towerV2) : base(towerV2)
    {
        
    }

    public override void OnStateEnter()
    {
        //Debug.Log("bulletForward");
        
        targetedPos = me.myStats.target.transform.position - me.transform.position;
    }
    public override void Tick()
    {
        me.transform.Translate(targetedPos.normalized * me.myStats.speed * 0.1f);
    }
    
    public override void OnStateExit()
    {
    }
}
