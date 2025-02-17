using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForwardState : StateTower
{
    public Bullet me;

    public Vector2 targetedPos;
    public BulletForwardState(Bullet bullet) : base(bullet)
    {
        me = bullet;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("bulletForward");
        targetedPos = me.myStats.target.transform.position - me.transform.position;
    }
    public override void Tick()
    {
        /*if (me.myStats.target != null)
        {
            targetedPos = me.myStats.target.transform.position - me.transform.position;
        }*/
        
        /*me.gameObject.transform.position = Vector2.MoveTowards(me.gameObject.transform.position, targetedPos,
            Time.deltaTime * me.myStats.speed);*/
        
        me.transform.Translate(targetedPos.normalized * me.myStats.speed * 0.1f);
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
