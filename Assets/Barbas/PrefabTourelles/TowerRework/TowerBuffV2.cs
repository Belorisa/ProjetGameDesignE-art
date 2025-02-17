using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuffV2 : TowerV2
{
    private Animator _animator;
    
    public int buffValue;

    public int[] buffValuePerLevel;

    public GameObject buffR;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        _animator = towerR.GetComponent<Animator>();

        SetState(new BuildingStateV2(this, new BuffTowerBehV2(this), myStats.constructionTime, _animator));

        width = 1.8f;
        height = 1.8f;
        
        //Debug.Log(Screen.height + ": " + height2);

        sellButton.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform, false);
        sellButton.GetComponent<Button>().onClick.AddListener(SellTower);

        levelUpButton.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform, false);
        levelUpButton.GetComponent<Button>().onClick.AddListener(LevelUp);
        
        uiTowerElements.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform, false);
        notEnoughGold.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform, false);
        buildingProgress.transform.parent.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform, false);

        showNotEnoughGoldCoroutine = ShowNotEnoughGold(0.7f);

        totalGoldSpentOnThisTower += myStats.goldCost;

        UpdateRange();
    }
    
    public override void LevelForAllTower() //pour toutes les tours avec un animator
    {
        AudioSource.PlayClipAtPoint(construction,new Vector3(transform.position.x,transform.position.y,-8), volume);
        SetState(new BuildingStateV2(this, new BuffTowerBehV2(this), myStats.constructionTime, _animator));
        
        OnHighlightExitLvlUpButton();

        gears.GetComponent<Gears>().playerI.GetComponent<Player>().SetState(new PlayerBuildingState(
            gears.GetComponent<Gears>().playerI.GetComponent<Player>(), myStats.constructionTime));
        
        if (towerLevel + 1 == maxLevel)
        {
            levelUpButton.GetComponent<Image>().sprite = shadedLvlUpButtonSprite;
            
            goldForNextLevelText.text = "";
        }
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
                                       + " buff dmg : " + buffValue + " ->  " + buffValuePerLevel[towerLevel] + '\n'
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
                                       + " buff dmg : " + buffValue + '\n'
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
            
                myStats = statsForEachLevel[i];

                _animator.SetInteger("Lvl", towerLevel + 1);
            
                LevelForAllTower();
                
                buffValue = buffValuePerLevel[i];
            
                Debug.Log("Level up made : Tower Buff");
                towerLevel++; //sa doit être à la fin
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}
