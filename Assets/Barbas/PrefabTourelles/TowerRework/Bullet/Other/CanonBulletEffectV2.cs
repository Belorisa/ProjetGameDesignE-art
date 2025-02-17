using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBulletEffectV2 : BulletMovments
{
    public TowerShoot tower;
    
    public Vector2 targetedPos;

    public float area;
    
    public CanonBulletEffectV2(TowerShoot towerV2, float area) : base(towerV2)
    {
        tower = towerV2;
        this.area = area;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
        
        targetedPos = me.myStats.target.transform.position;
    }
    public override void Tick()
    {
        if (Vector2.Distance(me.transform.position, targetedPos) <= 0.2f)
        {
            if (tower.bulletSound != null)
            {
                tower.bulletSound.source = me.gameObject.AddComponent<AudioSource>();
            
                tower.bulletSound.source.Play();
            }

            Collider2D[] ennemisInRange =
                Physics2D.OverlapCircleAll(me.gameObject.transform.position, area, me.gears.GetComponent<Gears>().ennemisLayer);

            foreach (var e in ennemisInRange)
            {
                if (e.gameObject.GetComponent<Ennemis>() != null)
                {
                    e.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);
                }
            }

            /*if (me.explosion != null)
            {
                me.FuncInstantiate(me.explosion, me.transform.position, me.explosion.transform);   
            }*/
            
            me.DestroyObject(me.gameObject);
        }
    }
    
    public override void OnStateExit()
    {
    }
}
