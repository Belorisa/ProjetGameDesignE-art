using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomRangEffect : BulletMovments
{
    public BulletBoomRangEffect(TowerV2 towerV2) : base(towerV2)
    {
        
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Bullet Effect : BoomRang");
    }
    public override void Tick()
    {
        
    }
    
    public override void OnStateExit()
    {
    }
    
    public override void OnCollision(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ennemis>() != null)
        {
            col.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);
        }
    }
}