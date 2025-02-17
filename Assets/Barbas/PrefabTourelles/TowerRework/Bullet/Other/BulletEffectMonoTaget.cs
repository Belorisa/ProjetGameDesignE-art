using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectMonoTaget : BulletMovments
{
    private ContactFilter2D t;
    
    private List<Collider2D> c = new List<Collider2D>();
    public BulletEffectMonoTaget(TowerV2 towerV2) : base(towerV2)
    {
        
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
                    col.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);
                    mono = true;
                }
            }
            me.DestroyObject(me.gameObject);
        }
    }
}
