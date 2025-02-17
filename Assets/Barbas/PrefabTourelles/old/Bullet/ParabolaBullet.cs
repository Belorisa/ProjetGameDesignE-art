using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaBullet : StateTower
{
    public Bullet me;

    public Vector2 targetedPos;

    public Vector2 startPos;

    public float progression;
    public ParabolaBullet(Bullet bullet) : base(bullet)
    {
        me = bullet;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Parabola Bullet");
        startPos = me.transform.position;
        targetedPos = me.myStats.target.transform.position;
    }
    public override void Tick()
    {
        progression += Time.deltaTime * me.myStats.speed;
        
        /*if (me.myStats.target != null)
        {
            targetedPos = me.myStats.target.transform.position;
        }*/
        
       me.transform.position = MathParabola.Parabola(startPos, targetedPos, 1, progression);

       if (Vector2.Distance(new Vector2(me.gameObject.transform.position.x, me.gameObject.transform.position.y),targetedPos) <= 0.05f)
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
