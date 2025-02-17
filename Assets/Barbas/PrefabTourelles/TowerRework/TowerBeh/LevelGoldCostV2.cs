using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGoldCostV2 : QuetionMarkUI
{
    public TowerV2 tower;
    
    public TextMeshProUGUI goldForNextLevelText;

    public float goldFromTower;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (tower.towerLevel < tower.maxLevel)
        {
            if (goldFromTower != tower.statsForEachLevel[tower.towerLevel].goldCost)
            {
                goldFromTower = tower.statsForEachLevel[tower.towerLevel].goldCost;
            
                goldForNextLevelText.text = goldFromTower.ToString();
            }
        }
        else
        {
            goldForNextLevelText.text = "Level Max";
        }
    }
}
