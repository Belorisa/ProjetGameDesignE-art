using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomRangeMovments : BulletMovments
{
    private TowerBoomRangV2 tower;
    
    public Vector2 targetedPos;

    public Vector2 startPos;

    public float progression;

    public bool returning;

    private float _parabolaHeigh;
    
    public BulletBoomRangeMovments(TowerV2 towerV2) : base(towerV2)
    {
        
    }
    
    public BulletBoomRangeMovments(TowerBoomRangV2 towerV2) : base(towerV2)
    {
        tower = towerV2;
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
                tower.towerR.GetComponent<SpriteRenderer>().sprite = tower.idleSprite;
            
                Debug.Log("idle sprite");
                
                me.DestroyObject(me.gameObject);
            }
        }

        if (!returning && progression >= 0.99f)
        {
            BoomRangReturn();
        }
        
        me.transform.Rotate(new Vector3(0,0,25));
    }
    
    public override void OnStateExit()
    {
    }

    public override void OnCollision(Collider2D col)
    {
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
