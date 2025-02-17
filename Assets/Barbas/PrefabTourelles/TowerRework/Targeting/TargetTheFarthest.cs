using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTheFarthest : TargetingSysteme
{
    public TargetTheFarthest(TowerV2 towerV2) : base(towerV2)
    {
        this.towerV2 = towerV2;
    }

    public override void OnStateEnter()
    { 
        //Debug.Log("target advanced");
    }
    public override void Tick()
    {
        Collider2D[] ennemisInRange =
            Physics2D.OverlapCircleAll(towerV2.towerR.gameObject.transform.position, towerV2.myStats.range, towerV2.gears.GetComponent<Gears>().ennemisLayer);

        if (ennemisInRange.Length != 0)
        {
            float minDistance = 0;

            foreach (var enemy in ennemisInRange)
            {
                float d = Vector2.Distance(towerV2.transform.position, enemy.transform.position);
                
                if (d > minDistance)
                {
                    target = enemy.gameObject;
                    minDistance = d;
                }
            }
        }
        else
        {
            target = null;
        }
    }
    
    public override void OnStateExit()
    {
    }
}
