using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEffect : BulletMovments
{
    private int _charges;

    private Sprite[] _sprites;
    
    private ContactFilter2D t;
    
    private List<Collider2D> c = new List<Collider2D>();
    
    public TrapEffect(TowerV2 towerV2, int charges, Sprite[] sprites) : base(towerV2)
    {
        _charges = charges;

        _sprites = sprites;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
        
        t.layerMask = me.gears.GetComponent<Gears>().ennemisLayer;
        
        switch (_charges)
        {
            case 1:
                me.GetComponent<SpriteRenderer>().sprite = _sprites[3];
                break;
            case 2:
                me.GetComponent<SpriteRenderer>().sprite = _sprites[2];
                break;
            case 3:
                me.GetComponent<SpriteRenderer>().sprite = _sprites[1];
                break;
            case 4:
                me.GetComponent<SpriteRenderer>().sprite = _sprites[0];
                break;
        }
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
            
            //Debug.Log(c.Count);
            
            bool mono = false;
            
            foreach (var hit in c)
            {
                if (!mono)
                {
                    col.gameObject.GetComponent<Ennemis>().TakeDamage(me.myStats.damage);

                    _charges -= 1;
            
                    if (_charges < 0)
                    {
                        me.DestroyObject(me.gameObject);
                    }
                    
                    switch (_charges)
                    {
                        case 1:
                            me.GetComponent<SpriteRenderer>().sprite = _sprites[3];
                            break;
                        case 2:
                            me.GetComponent<SpriteRenderer>().sprite = _sprites[2];
                            break;
                        case 3:
                            me.GetComponent<SpriteRenderer>().sprite = _sprites[1];
                            break;
                        case 4:
                            me.GetComponent<SpriteRenderer>().sprite = _sprites[0];
                            break;
                    }
                    
                    mono = true;
                }
            }
        }
    }
}