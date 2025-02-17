using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretBullet : StateTower
{
    public Bullet me;
    
    private ContactFilter2D t;
    
    private List<Collider2D> c = new List<Collider2D>();
    public PlayerTurretBullet(Bullet bullet) : base(bullet)
    {
        me = bullet;
    }

    public override void OnStateEnter()
    {
        t.layerMask = me.gears.GetComponent<Gears>().ennemisLayer;
    }
    public override void Tick()
    {
        
    }
    
    public override void OnStateExit()
    {
    }
    
    public override void OnCollision(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ennemis>() != null)
        {
            me.GetComponent<Collider2D>().OverlapCollider(t, c);

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