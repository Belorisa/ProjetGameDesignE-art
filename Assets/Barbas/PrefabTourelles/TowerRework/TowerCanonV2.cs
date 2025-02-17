using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCanonV2 : AimingTowersShoot
{
    public float area;

    public float[] areaPerLevel;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ShootingTowers(this)); //(this, null);
        
        targetingSysteme = new TargetAdvancedEnemy(this);
        
        bulletMovments = new BulletForwardMovments(this);

        bulletEffect = new CanonBulletEffectV2(this, area);
    }

    public override void OnHighlightEnterLvlUpButton()
    {
        lvlButtonUi.myTips.SetActive(true);

        if (towerLevel < maxLevel)
        {
            lvlButtonUi._description = "niveau suivant :" + " damage : "+ myStats.damages + " ->  " + statsForEachLevel[towerLevel].damages + '\n' +
                                       " vitesse des balles : " + myStats.bulletSpeed + " ->  " + statsForEachLevel[towerLevel].bulletSpeed + '\n'
                                       + " Portee : " + myStats.range + " ->  " + statsForEachLevel[towerLevel].range + '\n'
                                       + " Cadence De Tire : " + myStats.fireRate + " ->  " + statsForEachLevel[towerLevel].fireRate + '\n'
                                       + " AOE : " + area + " ->  " + areaPerLevel[towerLevel] + '\n'
                                       + "Temps de construction : " + statsForEachLevel[towerLevel].constructionTime;
        }
        else
        {
            lvlButtonUi._description = "Max level : " +  " Degats : " + myStats.damages + '\n'
                                       + '\n'
                                       + " Vitesse des balles : " + myStats.bulletSpeed + '\n'
                                       + '\n'
                                       + " Portee : " + myStats.range + '\n'
                                       + '\n'
                                       + " Candence de tire : " + myStats.fireRate + '\n'
                                       + '\n'
                                       + " AOE : " + area + '\n'
                                       + '\n'
                                       + "Temps de construction : " + "0";
        }

        lvlButtonUi._effectDescription.text = lvlButtonUi._description;

        lvlButtonUi.selected = true;
        
        OnHighlightEnterAnyButton();
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

                area = areaPerLevel[i];
                bulletEffect = new CanonBulletEffectV2(this, area);
            
                myStats = statsForEachLevel[i];
            
                LevelForAllTower();
            
                Debug.Log("Level up made : Tower Canon");
                towerLevel++; //sa doit être à la fin
                
                if (towerLevel == 1)
                {
                    bullet = Resources.Load<GameObject>("Bullets/BulletAOe 2");
                }
                
                if (towerLevel == 2)
                {
                    bullet = Resources.Load<GameObject>("Bullets/BulletAOe 3");
                }
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}
