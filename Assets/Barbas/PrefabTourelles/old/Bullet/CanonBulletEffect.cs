using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBulletEffect : StateTower
{
    public BulletAoe me;

    public Vector2 targetedPos;
    public CanonBulletEffect(BulletAoe bulletAoe) : base(bulletAoe)
    {
        me = bulletAoe;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Canon Bullet");
        targetedPos = me.myStats.target.transform.position;
    }
    public override void Tick()
    {
        /*if (me.myStats.target != null)
        {
            targetedPos = me.myStats.target.transform.position;
        }*/

        if (Vector2.Distance(me.transform.position, targetedPos) <= 0.3f)
        {
            Collider2D[] ennemisInRange =
                Physics2D.OverlapCircleAll(me.gameObject.transform.position, me.area, me.gears.GetComponent<Gears>().ennemisLayer);

            foreach (var e in ennemisInRange)
            {
                if (e.gameObject.GetComponent<Ennemis>() != null)
                {
                    e.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);
                }
            }

            if (me.explosion != null)
            {
                me.FuncInstantiate(me.explosion, me.transform.position, me.explosion.transform);   
            }
            
            me.DestroyObject(me.gameObject);
        }
    }
    
    public override void OnStateExit()
    {
    }
}
