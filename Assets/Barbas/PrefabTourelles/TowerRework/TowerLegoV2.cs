using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLegoV2 : AimingTowersShoot
{
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ShootingTowers(this)); //(this, null);
        
        targetingSysteme = new TargetAdvancedEnemy(this);
        
        bulletMovments = new BulletForwardMovments(this);
        
        bulletEffect = new BulletEffectMonoTaget(this);
    }

    public override void OnLevelUp(int i)
    {
        if (towerLevel < maxLevel)
        {
            if (gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds >=
                statsForEachLevel[i].goldCost)
            {
                
                gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds -=
                    statsForEachLevel[i].goldCost;
            
                totalGoldSpentOnThisTower += statsForEachLevel[i].goldCost;

                myStats = statsForEachLevel[i];

                LevelForAllTower();

                Debug.Log("Level up made : Tower Lego");
                towerLevel++; //sa doit être à la fin
                
                if (towerLevel == 1) //changement de la bullet(sa sert que à changer le sprite)
                {
                    bullet = Resources.Load<GameObject>("Bullets/BaseBullet2");
                }
                
                if (towerLevel == 2)
                {
                    bullet = Resources.Load<GameObject>("Bullets/BaseBullet3");
                }
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}
