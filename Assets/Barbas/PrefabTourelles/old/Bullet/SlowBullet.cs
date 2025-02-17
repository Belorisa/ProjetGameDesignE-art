using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBullet : StateTower
{
    public Bullet me;

    public Vector2 targetedPos;

    public float slowPower;

    public float slowTimer;
    
    public SlowBullet(Bullet bullet, float slowPower, float slowTimer) : base(bullet)
    {
        me = bullet;
        this.slowPower = slowPower;
        this.slowTimer = slowTimer;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
    }
    public override void Tick()
    {
        if (me.myStats.target != null)
        {
            targetedPos = me.myStats.target.transform.position - me.transform.position;
        }

        me.transform.Translate(targetedPos.normalized * me.myStats.speed * 0.1f);
    }
    
    public override void OnStateExit()
    {
    }
    
    public override void OnCollision(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ennemis>() != null)
        {
            col.gameObject.GetComponent<Ennemis>().SetDebuff(new SlowDeBuff(col.gameObject.GetComponent<Ennemis>(), slowPower, slowTimer));
            
            me.DestroyObject(me.gameObject);
        }
    }
}
