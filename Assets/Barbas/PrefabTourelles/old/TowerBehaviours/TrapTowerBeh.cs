using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTowerBeh : StateTowerLevelUp
{
    public TowerTrap tower;

    public float fireTimer;

    public GameObject closestWayPoint1, closestWayPoint2;

    public int indexWayPoint1, indexWayPoint2;

    public float progressionBeetweenWayPoints;

    public Vector2 pos;

    public TrapTowerBeh(TowerTrap tower) : base(tower)
    {
        this.tower = tower;
    }

    
    public override void OnStateEnter()
    {
        //Debug.Log("trapTower");

        float f = Mathf.Infinity;

        foreach (var point in tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints)
        {
            if (Vector2.Distance(point.transform.position, tower.transform.position) < f)
            {
                closestWayPoint1 = point;

                f = Vector2.Distance(point.transform.position, tower.transform.position);
            }
        }
        
        int i3 = 0;
        
        foreach (var waypoint in tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
            .waypoints)
        {
            if (waypoint == closestWayPoint1)
            {
                indexWayPoint1 = i3;
            }

            i3++;
        }

        if (tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>() //si y'a pas de point après celui le plus proche
                .waypoints.Length > indexWayPoint1 + 1)
        {
            if (indexWayPoint1 - 1 > 0) //si y'a pas de point avant le point le plus proche
            {
                if (Vector2.Angle(tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                                      .waypoints[indexWayPoint1 + 1].transform.position - tower.transform.position,
                        tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                            .waypoints[indexWayPoint1].transform.position - tower.transform.position) >
                    Vector2.Angle(tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                                      .waypoints[indexWayPoint1 - 1].transform.position - tower.transform.position,
                        tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                            .waypoints[indexWayPoint1].transform.position - tower.transform.position))
                {
                    closestWayPoint2 = tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                        .waypoints[indexWayPoint1 + 1];

                    indexWayPoint2 = indexWayPoint1 + 1;
                }
                else
                {
                    closestWayPoint2 = tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                        .waypoints[indexWayPoint1 - 1];

                    indexWayPoint2 = indexWayPoint1 - 1;
                }
            }
            else //ont prend celuii d'après
            {
                closestWayPoint2 = tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                    .waypoints[indexWayPoint1 + 1];

                indexWayPoint2 = indexWayPoint1 + 1;
            }
        }
        else //ont prend celui d'avant de toutes façon
        {
            closestWayPoint2 = tower.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                .waypoints[indexWayPoint1 - 1];

            indexWayPoint2 = indexWayPoint1 - 1;
        }
    }

    public override void Tick()
    {
        if (fireTimer <= 0)
        {
            if (indexWayPoint1 > indexWayPoint2)
            {
                progressionBeetweenWayPoints =
                    Vector2.Distance(tower.transform.position, closestWayPoint1.transform.position) /
                    Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position) + Random.Range(-0.1f, 0.1f); //Random.Range(-(tower.myStats.range * 0.1f), (tower.myStats.range * 0.1f)) pour prendre la range en compt
            
                pos = Vector2.Lerp(closestWayPoint1.transform.position, closestWayPoint2.transform.position, progressionBeetweenWayPoints);
            }
            else
            {
                progressionBeetweenWayPoints =
                    Vector2.Distance(tower.transform.position, closestWayPoint2.transform.position) /
                    Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position) + Random.Range(-0.1f, 0.1f);
            
                pos = Vector2.Lerp(closestWayPoint2.transform.position, closestWayPoint1.transform.position, progressionBeetweenWayPoints);
            }
            
            GameObject oui = tower.InstantiateBullet();
            
            oui.GetComponent<Bullet>().myStats = new BulletStats(tower.myStats.damages, tower.myStats.bulletSpeed,
                new TrapBeh(oui.GetComponent<Bullet>(), pos, tower.trapDuration), null);
            
            fireTimer = tower.myStats.fireRate;
        }

        fireTimer -= Time.deltaTime;
        fireTimer = Mathf.Clamp(fireTimer, 0, 100);
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
            Debug.Log("Level up made"); //peut être faire une fonction dans tower "baseLevelUp"
            tower.towerLevel++; //sa doit être à la fin
        }

        //ex: if i = 3 -> add state inhérent du premier
    }
}