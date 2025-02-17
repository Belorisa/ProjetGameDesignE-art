using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTargeting : TargetingSysteme
{
    private Sprite[] _sprites;
    
    public GameObject closestWayPoint1, closestWayPoint2;

    public int indexWayPoint1, indexWayPoint2;

    public float progressionBeetweenWayPoints;

    public Vector2 pos;
    
    private float _angle;
    
    public TrapTargeting(TowerV2 towerV2, Sprite[] sprites) : base(towerV2)
    {
        this.towerV2 = towerV2;

        _sprites = sprites;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("trap Target");
        
         float f = Mathf.Infinity;

        foreach (var point in towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints)
        {
            if (Vector2.Distance(point.transform.position, towerV2.transform.position) < f)
            {
                closestWayPoint1 = point;

                f = Vector2.Distance(point.transform.position, towerV2.transform.position);
            }
        }
        
        int i3 = 0;
        
        foreach (var waypoint in towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints) //trouver l'index
        {
            if (waypoint == closestWayPoint1)
            {
                indexWayPoint1 = i3;
            }

            i3++;
        }

        if (towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>() //si y'a pas de point après celui le plus proche
                .waypoints.Length > indexWayPoint1 + 1)
        {
            if (indexWayPoint1 - 1 > 0) //si y'a pas de point avant le point le plus proche
            {
                if (Vector2.Angle(towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                                      .waypoints[indexWayPoint1 + 1].transform.position - towerV2.transform.position,
                        towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                            .waypoints[indexWayPoint1].transform.position - towerV2.transform.position) >
                    Vector2.Angle(towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>()
                                      .waypoints[indexWayPoint1 - 1].transform.position - towerV2.transform.position,
                        towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints[indexWayPoint1].transform.position - towerV2.transform.position))
                {
                    closestWayPoint2 = towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints[indexWayPoint1 + 1];

                    indexWayPoint2 = indexWayPoint1 + 1;
                }
                else
                {
                    closestWayPoint2 = towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints[indexWayPoint1 - 1];

                    indexWayPoint2 = indexWayPoint1 - 1;
                }
            }
            else //ont prend celuii d'après
            {
                closestWayPoint2 = towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints[indexWayPoint1 + 1];

                indexWayPoint2 = indexWayPoint1 + 1;
            }
        }
        else //ont prend celui d'avant de toutes façon
        {
            closestWayPoint2 = towerV2.gears.GetComponent<Gears>().pathManager.GetComponent<WaveManagerB>().waypoints[indexWayPoint1 - 1];

            indexWayPoint2 = indexWayPoint1 - 1;
        }

        if (indexWayPoint1 > indexWayPoint2)
        {
            progressionBeetweenWayPoints =
                Vector2.Distance(towerV2.transform.position, closestWayPoint1.transform.position) /
                Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position);
            
             pos = Vector2.Lerp(closestWayPoint1.transform.position, closestWayPoint2.transform.position, progressionBeetweenWayPoints);
        }
        else
        {
            progressionBeetweenWayPoints =
                Vector2.Distance(towerV2.transform.position, closestWayPoint2.transform.position) /
                Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position);
            
            pos = Vector2.Lerp(closestWayPoint2.transform.position, closestWayPoint1.transform.position, progressionBeetweenWayPoints);
        }
        
        Vector3 relative = towerV2.transform.InverseTransformPoint(pos);
        _angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        
        Debug.Log(_angle + " " + pos);

        if (_angle >= 0 && _angle <= 45 || _angle <= 0 && _angle >= -45)
        {
            towerV2.towerR.GetComponent<SpriteRenderer>().sprite = _sprites[0];
        }else if (_angle > 45 && _angle <= 135)
        {
            towerV2.towerR.GetComponent<SpriteRenderer>().sprite = _sprites[1];
        }else if (_angle > 135 || _angle < -135)
        {
            towerV2.towerR.GetComponent<SpriteRenderer>().sprite = _sprites[2];
        }
        else if (_angle < -45 && _angle > -135)
        {
            towerV2.towerR.GetComponent<SpriteRenderer>().sprite = _sprites[3];
        }
    }

    public override void Tick()
    {
        if (indexWayPoint1 > indexWayPoint2)
        {
            progressionBeetweenWayPoints =
                Vector2.Distance(towerV2.transform.position, closestWayPoint1.transform.position) /
                Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position) + Random.Range(-0.1f, 0.1f); //Random.Range(-(tower.myStats.range * 0.1f), (tower.myStats.range * 0.1f)) pour prendre la range en compt
            
            pos = Vector2.Lerp(closestWayPoint1.transform.position, closestWayPoint2.transform.position, progressionBeetweenWayPoints);
        }
        else
        {
            progressionBeetweenWayPoints =
                Vector2.Distance(towerV2.transform.position, closestWayPoint2.transform.position) /
                Vector2.Distance(closestWayPoint1.transform.position, closestWayPoint2.transform.position) + Random.Range(-0.1f, 0.1f);
            
            pos = Vector2.Lerp(closestWayPoint2.transform.position, closestWayPoint1.transform.position, progressionBeetweenWayPoints);
        }
    }

    public override void OnStateExit()
    {
    }
}
