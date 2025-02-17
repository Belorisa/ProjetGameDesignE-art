using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStateTowerShoot : BuildingStateV2
{
    private TowerShoot _towerShoot;
    
    public TargetingSysteme targetingSysteme;
    
    public BuildingStateTowerShoot(TowerShoot tower, StateTower towerWhenFinished, float constructionTime) : base(tower, towerWhenFinished, constructionTime)
    {
        _towerShoot = tower;
        
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;

        this.constructionTime = constructionTime;
    }
    
    public BuildingStateTowerShoot(TowerShoot tower, StateTower towerWhenFinished, float constructionTime, Sprite nextSprite) : base(tower, towerWhenFinished, constructionTime, nextSprite)
    {
        _towerShoot = tower;
        
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;

        this.constructionTime = constructionTime;

        this.nextSprite = nextSprite;
    }
    
    public override void OnStateEnter()
    { 
        targetingSysteme = _towerShoot.targetingSysteme.ShallowCopy();

        _towerShoot.targetingSysteme = null;
        
        //Debug.Log(targetingSysteme);
        
        buildingProgress = tower.buildingProgress;
        
        baseScalebuildingProgress = buildingProgress.transform.localScale;
        
        buildingProgress.transform.parent.gameObject.SetActive(true);
        
        if (constructionTime == 0)
        {
            //Debug.Log("construction time = 0");
            tower.SetState(towerWhenFinished);
        }
        else
        {
            tower.HideTowerInfos();
        }

        //Debug.Log("Building Shoot Towers...");

        //tower.mySounds.PlaySound("buildingSound");
    }

    public override void OnStateExit()
    {
        _towerShoot.targetingSysteme = targetingSysteme;
        
        if (nextSprite != null)
        {
            if (tower.myStats.sprite != null)
            {
                tower.towerR.GetComponent<SpriteRenderer>().sprite = tower.myStats.sprite;
            }
        }
        
        //tower.mySounds.StopASound("buildingSound");
        
        buildingProgress.transform.localScale = baseScalebuildingProgress;
        buildingProgress.transform.parent.gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(tower.finishSound, new Vector3(tower.transform.position.x,tower.transform.position.y,-8), 1.0f);

    }
}
