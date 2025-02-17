using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingState : StateTower
{
    public Player me;
    
    public float constructionTime;

    public float progress;

    public IEnumerator buildingCoroutine;
    
    public PlayerBuildingState(Player me, float constructionTime) : base(me)
    {
        this.me = me;
        
        this.constructionTime = constructionTime;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Player is Building...");
        buildingCoroutine = BuildingAnimation();
        
        me.StartCoroutine(buildingCoroutine);
    }
    public override void Tick()
    {
        progress += Time.deltaTime; //* skillTree ReducedBuildTime
        
        
        if (progress >= constructionTime)
        {
            me.StopCoroutine(buildingCoroutine);
        
            me.animator.SetBool("AboutToBuild", false);
        
            me.animator.SetBool("IsBuilding", false);
            
            if (progress >= constructionTime + 0.5f)
            {
                me.SetState(new PlayerStateMoov(me));
            }
        }
    }

    public IEnumerator BuildingAnimation()
    {
        me.animator.SetBool("AboutToBuild", true);

        yield return new WaitForSeconds(0.5f);
        
        me.animator.SetBool("IsBuilding", true);
        
        me.animator.SetBool("AboutToBuild", false);
    }
    
    public override void OnStateExit()
    {
        
    }
}