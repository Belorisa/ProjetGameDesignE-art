using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehV2 : BulletMovments
{
    public Vector2 targetedPos;

    private float _trapDuration;

    public TrapBehV2(TowerV2 towerV2, float trapDurationTower, Vector2 trapTagetPos) : base(towerV2)
    {
        _trapDuration = trapDurationTower;

        targetedPos = trapTagetPos;
    }
    
    /*public TrapBehV2 Copy(float d, Vector2 pos)
    {
        TrapBehV2 i = (TrapBehV2) MemberwiseClone();

        i.trapDuration = d;

        i.targetedPos = pos;
        
        return i;
    }*/

    public override void OnStateEnter()
    {
        //Debug.Log("bulletForward");
        me.transform.position = targetedPos;
    }
    public override void Tick()
    {
        _trapDuration -= Time.deltaTime;

        if (_trapDuration <= 0)
        {
            me.DestroyObject(me.gameObject);
        }
    }
    
    public override void OnStateExit()
    {
    }
}
