using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStateV2 : StateTower
{
    public TowerV2 tower;

    public StateTower towerWhenFinished;
    
    public GameObject buildingProgress;

    public float progress;

    public float constructionTime;

    public Vector3 baseScalebuildingProgress;

    public Sprite nextSprite;
    
    private Animator _animator;
    
    public BuildingStateV2(TowerV2 tower, StateTower towerWhenFinished, float constructionTime) : base(tower)
    {
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;

        this.constructionTime = constructionTime;
    }
    
    public BuildingStateV2(TowerV2 tower, StateTower towerWhenFinished, float constructionTime, Animator animator) : base(tower)
    {
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;

        this.constructionTime = constructionTime;

        _animator = animator;
    }
    
    public BuildingStateV2(TowerV2 tower, StateTower towerWhenFinished, float constructionTime, Sprite nextSprite) : base(tower)
    {
        this.tower = tower;
        this.towerWhenFinished = towerWhenFinished;

        this.constructionTime = constructionTime;

        this.nextSprite = nextSprite;
    }

    public override void OnStateEnter()
    {
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

        //Debug.Log("Building...");

        //tower.mySounds.PlaySound("buildingSound");

        if (_animator != null)
        {
            _animator.enabled = false;
        }
    }
    public override void Tick()
    {
        progress += Time.deltaTime;

        buildingProgress.transform.localScale = new Vector3(progress / constructionTime, buildingProgress.transform.localScale.y, buildingProgress.transform.localScale.z);

        buildingProgress.transform.parent.position =
            tower.gears.GetComponent<Gears>().cam.WorldToScreenPoint(new Vector2(tower.transform.position.x, tower.transform.position.y) + tower.CircleAdvance(0.31f));
        
        if (progress >= constructionTime)
        {
            stateMachine.SetState(towerWhenFinished);
        }
    }
    
    public override void OnStateExit()
    {
        if (tower.myStats.sprite != null)
        {
            tower.towerR.GetComponent<SpriteRenderer>().sprite = tower.myStats.sprite;
                
            Debug.Log("update Sprite");
        }

        //tower.mySounds.StopASound("buildingSound");
        
        if (_animator != null)
        {
            _animator.enabled = true;
        }
        
        buildingProgress.transform.localScale = baseScalebuildingProgress;
        buildingProgress.transform.parent.gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(tower.finishSound, new Vector3(tower.transform.position.x,tower.transform.position.y,-8), 1.0f);

    }
}
