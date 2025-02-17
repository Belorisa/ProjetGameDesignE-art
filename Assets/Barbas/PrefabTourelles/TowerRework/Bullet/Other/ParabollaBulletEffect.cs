using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabollaBulletEffect : BulletMovments
{
    private ContactFilter2D t;
    
    private List<Collider2D> c = new List<Collider2D>();

    public ParabollaBulletEffect(TowerV2 towerV2) : base(towerV2)
    {
        
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
        //me.GetComponent<Collider2D>().enabled = false;
        
        t.layerMask = me.gears.GetComponent<Gears>().ennemisLayer;
    }
    public override void Tick()
    {
        /*if (Vector2.Distance(new Vector2(me.transform.position.x, me.transform.position.y), targetedPos) < 0.1f)
        {
            me.GetComponent<Collider2D>().enabled = true;
        }*/
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