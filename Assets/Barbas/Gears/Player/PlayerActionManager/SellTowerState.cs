using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTowerState : StateTower
{
    public PlayerActionManager playerActionManager;
    
    public RaycastHit2D hit;
    
    public SellTowerState(PlayerActionManager playerActionManager) : base(playerActionManager)
    {
        this.playerActionManager = playerActionManager;
    }

    public override void OnStateEnter()
    { 
        //Debug.Log("Sell State entered");
        //changer le curseur
    }
    public override void Tick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            hit = Physics2D.Raycast(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition), 
                new Vector3(0,0,-1), Mathf.Infinity, playerActionManager.gears.GetComponent<Gears>().TowerLayer);

            if (hit.collider.gameObject.GetComponent<TowerV2>() != null)
            {
                playerActionManager.golds += hit.collider.gameObject.GetComponent<TowerV2>().totalGoldSpentOnThisTower * 0.75f;
                playerActionManager.DestroyObject(hit.collider.gameObject);
                
                Debug.Log(hit.collider.gameObject.name + " sold for : " + hit.collider.gameObject.GetComponent<TowerV2>().totalGoldSpentOnThisTower * 0.75f);
                
                playerActionManager.SetState(null);
            }
        }
    }
    
    public override void OnStateExit()
    {
    }
}
