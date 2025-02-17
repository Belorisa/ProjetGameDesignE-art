using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGoldCost : MonoBehaviour
{
    public Tower tower;
    
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
            
                goldForNextLevelText.text = "Cost : " + goldFromTower;
            }
        }
    }
}
