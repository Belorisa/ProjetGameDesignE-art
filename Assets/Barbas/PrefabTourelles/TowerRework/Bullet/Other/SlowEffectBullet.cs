using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffectBullet :BulletMovments
{
    public float slowPower;

    public float slowTimer;
    
    private ContactFilter2D t;
    
    private List<Collider2D> c = new List<Collider2D>();
    
    public SlowEffectBullet(TowerV2 towerV2, float slow, float slowTimerTower) : base(towerV2)
    {
        slowPower = slow;

        slowTimer = slowTimerTower;
    }
    public override void OnStateEnter()
    {
        //Debug.Log("");
        
        t.layerMask = me.gears.GetComponent<Gears>().ennemisLayer;
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
            me.GetComponent<Collider2D>().OverlapCollider(t, c);
            
            //Debug.Log(c.Count);
            
            bool mono = false;
            
            foreach (var hit in c)
            {
                if (!mono)
                {
                    col.gameObject.GetComponent<Ennemis>().SetDebuff(new SlowDeBuff(col.gameObject.GetComponent<Ennemis>(), slowPower, slowTimer));
                    mono = true;
                }
            }
            me.DestroyObject(me.gameObject);
        }
    }
}