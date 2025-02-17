using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : StateTowerLevelUp
{
    public TowerCanon tower;

    public GameObject closestTarget;

    public float fireTimer;
    public CanonTower(TowerCanon tower) : base(tower)
    {
        this.tower = tower;
    }
    
    public override void OnStateEnter()
    {
        //Debug.Log("");
    }
    public override void Tick()
    {
        Collider2D[] ennemisInRange =
            Physics2D.OverlapCircleAll(tower.gameObject.transform.position, tower.myStats.range, tower.gears.GetComponent<Gears>().ennemisLayer);

        if (ennemisInRange.Length != 0)
        {
            int maxWayPoint = 0;

            float maxProgression = 0;

            foreach (var enemy in ennemisInRange)
            {
                if (enemy.GetComponent<EnemyWayPoint2>().currentWaypoint > maxWayPoint)
                {
                    closestTarget = enemy.gameObject;
                    
                    maxWayPoint = enemy.GetComponent<EnemyWayPoint2>().currentWaypoint;
                    
                    maxProgression = enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath;
                }
                else if (enemy.GetComponent<EnemyWayPoint2>().currentWaypoint == maxWayPoint)
                {
                    if (enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath > maxProgression)
                    {
                        closestTarget = enemy.gameObject;

                        maxProgression = enemy.GetComponent<EnemyWayPoint2>().currentTimeOnPath;
                    }
                }
            }
        }
        else
        {
            closestTarget = null;
        }

        if (closestTarget != null)
        {
            if (fireTimer <= 0)
            {
                GameObject oui = tower.InstantiateBullet(); //la méthode instantiate c dans monobehaviour
                oui.GetComponent<Bullet>().myStats = new BulletStats(tower.myStats.damages, tower.myStats.bulletSpeed, 
                    new BulletForwardState(oui.GetComponent<Bullet>()), closestTarget, new CanonBulletEffect(oui.GetComponent<BulletAoe>()));

                oui.GetComponent<BulletAoe>().explosion = tower.explosion;

                oui.GetComponent<BulletAoe>().area = tower.area;
                
                fireTimer = tower.myStats.fireRate;
            }
        }

        fireTimer -= Time.deltaTime;
        Mathf.Clamp(fireTimer, 0, 100);
    }
    
    public override void OnStateExit()
    {
    }
    
    public override void OnLevelUp(int i)
    {
        if (tower.towerLevel < tower.maxLevel &&  //si la tour est pas max lvl && ont a les gold && le joueur est asser prêt
            tower.gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds >= tower.statsForEachLevel[i].goldCost)
        {
            tower.gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds -=
                tower.statsForEachLevel[i].goldCost;
            
            tower.totalGoldSpentOnThisTower += tower.statsForEachLevel[i].goldCost;
            
            tower.myStats = tower.statsForEachLevel[i];
            tower.area = tower.areaPerlevel[i];
            Debug.Log("Level up made");
            tower.towerLevel++; //sa doit être à la fin
        }

        //ex: if i = 3 -> add state inhérent du premier
    }
}
