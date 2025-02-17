using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTowerBehV2 : StateTower
{
    public TowerBuffV2 tower;

    public List<GameObject> towersBuffed = new List<GameObject>();
    
    public List<GameObject> buffRs = new List<GameObject>();
    
    public BuffTowerBehV2(TowerBuffV2 tower) : base(tower)
    {
        this.tower = tower;
    }

    
    public override void OnStateEnter()
    {
        //Debug.Log("");
    }

    public override void Tick()
    {
        Collider2D[] towersInRange =
            Physics2D.OverlapCircleAll(tower.gameObject.transform.position, tower.myStats.range, tower.gears.GetComponent<Gears>().TowerLayer);

        foreach (var TIR in towersInRange)
        {
            if (TIR.GetComponent<TowerV2>() != null && !towersBuffed.Contains(TIR.gameObject) && TIR.gameObject != tower.gameObject)
            {
                //Debug.Log("BuffV2 - " + TIR.name);
                TIR.GetComponent<TowerV2>().myStats.damages += tower.buffValue; //+ tower.myStats.damages?

                GameObject go = tower.FuncInstantiate(tower.buffR, new Vector3(TIR.transform.position.x, TIR.transform.position.y + 1, -1.1f), tower.buffR.transform);
                
                buffRs.Add(go);
                
                towersBuffed.Add(TIR.gameObject);

                EventManager.current.OnSellTower += OnTowerSold;
            }
        }

        /*for (int i = 0; i < towersBuffed.Count; i++)
        {
            if (!((IList) towersInRange).Contains(towersBuffed[i].GetComponent<Collider2D>()))
            {
                towersBuffed[i].GetComponent<TowerV2>().myStats.damages -= tower.buffValue;
                
                tower.DestroyObject(buffRs[i]);
                
                towersBuffed.Remove(towersBuffed[i]);
            }
        }*/
    }
    
    private void OnTowerSold(int id) //l'id est determiné quand ont appelle l'event
    {
        for (int i = 0; i < towersBuffed.Count; i++)
        {
            int index = 0;
            
            if (tower.GetComponent<TowerV2>().id == id)
            {
                DestroyBuffRepresentation(index);

                towersBuffed.Remove(towersBuffed[index]);
                Debug.Log("watch tower sold");
            }

            index++;
        }
    }

    private void DestroyBuffRepresentation(int index)
    {
        tower.DestroyObject(buffRs[index]);
    }

    public override void OnStateExit()
    {
        /*foreach (var buffR in buffRs)
        {
           tower.DestroyObject(buffR);
        }*/
        
        for (int i = 0; i < towersBuffed.Count; i++)
        {
            towersBuffed[i].GetComponent<TowerV2>().myStats.damages -= tower.buffValue;
                
            tower.DestroyObject(buffRs[i]);
        }
    }
}
