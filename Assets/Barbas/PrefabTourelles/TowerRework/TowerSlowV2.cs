using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlowV2 : AimingTowersShoot
{
    public TowerSlowSpecialStats mySpecialStats;

    public TowerSlowSpecialStats[] mySpecialStatsPerLevel;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ShootingTowers(this));
        
        targetingSysteme = new TargetAdvancedEnemy(this);
        
        bulletMovments = new BulletForwardMovments(this);

        bulletEffect = new SlowEffectBullet(this, mySpecialStats.slow, mySpecialStats.slowTimer);
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
                                       + " Slow : " + mySpecialStats.slow + " ->  " + mySpecialStatsPerLevel[towerLevel].slow + '\n'
                                       + " SlowDuration : " + mySpecialStats.slowTimer + " ->  " + mySpecialStatsPerLevel[towerLevel].slowTimer + '\n'
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
                                       + " Slow : " + mySpecialStats.slow + '\n'
                                       + '\n'
                                       + " SlowDuration : " + mySpecialStats.slowTimer + '\n'
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

                mySpecialStats = mySpecialStatsPerLevel[i];

                bulletEffect = new SlowEffectBullet(this, mySpecialStats.slow, mySpecialStats.slowTimer);
            
                myStats = statsForEachLevel[i];
            
                LevelForAllTower();
            
                Debug.Log("Level up made : Tower Lego");
                towerLevel++; //sa doit être à la fin
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}