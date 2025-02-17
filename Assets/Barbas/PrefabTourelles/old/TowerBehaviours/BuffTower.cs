using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffTower : StateTowerLevelUp
{
    public TowerBuff tower;
    
    public List<GameObject> towersBuffed = new List<GameObject>();

    public BuffTower(TowerBuff tower) : base(tower)
    {
        this.tower = tower;
    }

    
    public override void OnStateEnter()
    {
        //Debug.Log("Buff tower");
    }

    public override void Tick()
    {
        Collider2D[] towersInRange =
            Physics2D.OverlapCircleAll(tower.gameObject.transform.position, tower.myStats.range, tower.gears.GetComponent<Gears>().TowerLayer);

        foreach (var TIR in towersInRange)
        {
            if (TIR.GetComponent<Tower>() != null && !towersBuffed.Contains(TIR.gameObject) && TIR.gameObject != tower.gameObject)
            {
                //Debug.Log("Buff");
                TIR.GetComponent<Tower>().myStats.damages += tower.buffEffect; //+ tower.myStats.damages?
                towersBuffed.Add(TIR.gameObject);
            }
        }

        for (int i = 0; i < towersBuffed.Count; i++)
        {
            if (!towersInRange.Contains(towersBuffed[i].GetComponent<Collider2D>()))
            {
                towersBuffed[i].GetComponent<Tower>().myStats.damages -= tower.buffEffect;
                towersBuffed.Remove(towersBuffed[i]);
            }
        }
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
            Debug.Log("Level up made");
            tower.towerLevel++; //sa doit être à la fin
        }

        //ex: if i = 3 -> add state inhérent du premier
    }
}
