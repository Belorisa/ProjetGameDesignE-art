using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : StateTower
{
    public Tower tower;

    public StateTowerLevelUp towerWhenFinished;
    
    public GameObject buildingProgress;

    public float progress;
    
    public BuildingState(Tower tower, StateTowerLevelUp towerWhenFinished) : base(tower)
    {
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Building...");
        
        buildingProgress = tower.buildingProgress;
        
        buildingProgress.transform.parent.gameObject.SetActive(true);
    }
    public override void Tick()
    {
        progress += Time.deltaTime;

        buildingProgress.transform.localScale = new Vector3(progress / tower.constructionTime, buildingProgress.transform.localScale.y, buildingProgress.transform.localScale.z); 
        
        buildingProgress.transform.parent.position = tower.gears.GetComponent<Gears>().cam.WorldToScreenPoint(tower.showInfoHere[2].transform.position);
        
        if (progress >= tower.constructionTime)
        {
            tower.mainState = towerWhenFinished;
            
            stateMachine.SetState(tower.mainState);
        }
    }
    
    public override void OnStateExit()
    {
        buildingProgress.transform.parent.gameObject.SetActive(false);
    }
}
