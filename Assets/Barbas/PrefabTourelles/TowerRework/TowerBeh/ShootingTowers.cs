using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowers : StateTower
{
   public TowerShoot tower;

   public float fireTimer;
    
    public ShootingTowers(TowerShoot tower) : base(tower)
    {
        this.tower = tower;
    }

    
    public override void OnStateEnter()
    {
        //Debug.Log("ShootingTower Up - " + tower.name);
    }

    public override void Tick()
    {
        tower.targetingSysteme.Tick();

        if (tower.targetingSysteme.target != null)
        {
            if (fireTimer <= 0)
            {
                tower.TowerShootSystem();
                
                //tower.mySounds.PlaySound("Shoot");
                
                fireTimer = tower.myStats.fireRate;
            }
        }

        fireTimer -= Time.deltaTime;
        fireTimer = Mathf.Clamp(fireTimer, 0, 100);
    }

    public override void OnStateExit()
    {
    }
}
