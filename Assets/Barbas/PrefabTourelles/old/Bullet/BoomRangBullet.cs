using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRangBullet : StateTower
{
    public Bullet me;

    public Vector2 targetedPos;

    public Vector2 startPos;

    public float progression;

    public bool returning;

    private float _parabolaHeigh;
    
    public BoomRangBullet(Bullet bullet) : base(bullet)
    {
        me = bullet;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("BoomRang Bullet");
        startPos = me.transform.position;
        _parabolaHeigh = 1f;
        me.gameObject.tag = "BoomRang";
    }
    public override void Tick()
    {
        progression += Time.deltaTime * me.myStats.speed;
        
        if (me.myStats.target != null)
        {
            targetedPos = me.myStats.target.transform.position;
        }

        if (!returning)
        {
            me.transform.position = MathParabola.Parabola(startPos, targetedPos, _parabolaHeigh, progression);
        }
        else
        {
            me.transform.position = MathParabola.Parabola(targetedPos, startPos, -_parabolaHeigh, progression);

            if (Vector2.Distance(me.gameObject.transform.position, startPos) <= 0.2f)
            {
                me.DestroyObject(me.gameObject);
            }
        }
        
        if (Vector2.Distance(new Vector2(me.gameObject.transform.position.x, me.gameObject.transform.position.y),targetedPos) <= 0.05f && !returning)
        {
           BoomRangReturn();
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
        }

        if (col.gameObject == me.myStats.target)
       {
           BoomRangReturn();
       }
    }

    public void BoomRangReturn()
    {
        returning = true;
        progression = 0;
    }
}
