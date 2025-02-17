using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrapV2 : TowerShoot
{
    public float trapDuration;

    public float[] trapDurationForEachLevel;

    public int charges;

    public int[] chargesPerLevel;

    public TrapTargeting t;

    public Sprite[] spritesTrap;

    public Sprite[] spritesTrapTower;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ShootingTowers(this));

        t = new TrapTargeting(this, spritesTrapTower);
        
        targetingSysteme = t;

        targetingSysteme.target = gameObject;
        
        targetingSysteme.OnStateEnter();
        
        bulletMovments = null;
        
        bulletEffect = new TrapEffect(this, charges, spritesTrap);
    }
    
    public override void TowerShootSystem()
    {
        GameObject baseBullet = InstantiateBullet();
        
        bulletMovments = new TrapBehV2(this, trapDuration, t.pos);

        baseBullet.GetComponent<BulletV2>().myStats = new BulletStatsV2(myStats.damages, myStats.bulletSpeed,
            bulletMovments.ShallowCopy(), targetingSysteme.target, bulletEffect.ShallowCopy());

        baseBullet.GetComponent<BulletV2>().myStats.movments.me = baseBullet.GetComponent<BulletV2>();
        
        baseBullet.GetComponent<BulletV2>().myStats.effect.me = baseBullet.GetComponent<BulletV2>();
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
                                       + " Trap duration : " + trapDuration + " ->  " + trapDurationForEachLevel[towerLevel] + '\n'
                                       + " Charges : " + charges + " ->  " + chargesPerLevel[towerLevel] + '\n'
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
                                       + " Trap duration : " + trapDuration + '\n'
                                       + '\n'
                                       + " Charges : " + charges + '\n'
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

                trapDuration = trapDurationForEachLevel[i];

                charges = chargesPerLevel[i];
                bulletEffect = new TrapEffect(this, charges, spritesTrap);

                totalGoldSpentOnThisTower += statsForEachLevel[i].goldCost;
            
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
