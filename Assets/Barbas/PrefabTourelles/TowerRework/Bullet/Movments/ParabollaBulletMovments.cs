using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabollaBulletMovments :BulletMovments
{
    private Vector2 targetedPos;

    private Vector2 startPos;

    private float progression;
    
    public ParabollaBulletMovments(TowerV2 towerV2) : base(towerV2)
    {
        
    }

    public override void OnStateEnter()
    {
        //Debug.Log("ParabollaBullet");

        startPos = me.transform.position;
        targetedPos = me.myStats.target.transform.position;
    }
    public override void Tick()
    {
        progression += Time.deltaTime * me.myStats.speed;

        me.transform.position = MathParabola.Parabola(startPos, targetedPos, 1, progression);

        if (Vector2.Distance(new Vector2(me.gameObject.transform.position.x, me.gameObject.transform.position.y),targetedPos) <= 0.05f)
        {
            me.DestroyObject(me.gameObject);
        }
    }
    
    public override void OnStateExit()
    {
    }
}