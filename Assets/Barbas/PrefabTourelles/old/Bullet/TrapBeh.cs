using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBeh : StateTower
{
    public Bullet me;

    public Vector2 targetedPos;

    private float trapDuration;

    public TrapBeh(Bullet bullet, Vector2 pos, float trapDuration) : base(bullet)
    {
        me = bullet;
        targetedPos = pos;

        this.trapDuration = trapDuration;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("TrapBeh");

        me.transform.position = targetedPos;
    }

    public override void Tick()
    {
        trapDuration -= Time.deltaTime;

        if (trapDuration <= 0)
        {
            me.DestroyObject(me.gameObject);
        }
    }

    public override void OnStateExit()
    {
    }

    public override void OnCollision(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ennemis>() != null)
        {
            col.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);

            me.DestroyObject(me.gameObject);
        }
    }
}
