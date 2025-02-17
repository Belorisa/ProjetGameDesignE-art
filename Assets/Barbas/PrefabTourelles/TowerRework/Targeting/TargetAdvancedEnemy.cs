using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetAdvancedEnemy : TargetingSysteme
{
    public TargetAdvancedEnemy(TowerV2 towerV2) : base(towerV2)
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

        if (ennemisInRange.Length > 0)
        {
            int maxWayPoint = -1;

            float maxProgression = 0;

            foreach (var enemy in ennemisInRange)
            {
                //if (Vector2.Distance(towerV2.transform.position, enemy.transform.position) < towerV2.myStats.range)
                //{
                if (enemy.GetComponent<EnemyWayPoint2>().currentWaypoint > maxWayPoint)
                {
                    target = enemy.gameObject;
                    
                    maxWayPoint = enemy.GetComponent<EnemyWayPoint2>().currentWaypoint;
                    
                    maxProgression = enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath;
                }
                else if (enemy.GetComponent<EnemyWayPoint2>().currentWaypoint == maxWayPoint)
                {
                    if (enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath > maxProgression)
                    {
                        target = enemy.gameObject;
                        maxProgression = enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath; 
                    }
                }
                //}
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
