using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimingTowersShoot : TowerShoot
{ 
    private float _angle;

    private int _currentAiming;

    private float _diff;

    public Sprite[] sprites;

    public AimingSpritePerLvl[] spritesPerLvl;

    public Vector2[] localPosFirePoint;
    
    new void Update()
    {
        _diff = 2;
        
        currentState.Tick();

        ShowTowerInfos();
      
        if (targetingSysteme?.target != null)
        {
            Vector3 relative = transform.InverseTransformPoint(targetingSysteme.target.transform.position);
            _angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;

            /*Vector2 dir = targetingSysteme.target.transform.position - transform.position;
            firePoint.localPosition = dir.normalized * 1;*/
        }

        if (_angle < 180 && _angle > 135 + _diff)
        {
            UpdateAiming(0);
        }
        else if (_angle < 135 - _diff && _angle >= 90 + _diff)
        {
            UpdateAiming(1);
        }
        else if (_angle < 90 - _diff && _angle >= 45 + _diff)
        {
            UpdateAiming(2);
        }
        else if (_angle < 45 - _diff && _angle >= 0 + _diff)
        {
            UpdateAiming(3);
        }
        else if (_angle < 0 - _diff && _angle >= -45 + _diff)
        {
            UpdateAiming(4);
        }
        else if (_angle < -45 - _diff && _angle >= -90 + _diff)
        {
            UpdateAiming(5);
        }
        else if (_angle < -90 - _diff && _angle > -135 + _diff)
        {
            UpdateAiming(6);
        }
        else if (_angle < -135 - _diff && _angle > -180)
        {
            UpdateAiming(7);
        }

        //Debug.Log("Update - " + gameObject.name);
    }

    public void UpdateAiming(int i)
    {
        if (_currentAiming != i)
        {
            towerR.GetComponent<SpriteRenderer>().sprite = sprites[i]; 
            firePoint.localPosition = localPosFirePoint[i];
            
            //Debug.Log(targetingSysteme.target);
        }

        _currentAiming = i;
    }
    
    public override void LevelForAllTower()
    {
        OnHighlightExitLvlUpButton();
        
        if (towerLevel < maxLevel)
        {
            sprites = spritesPerLvl[towerLevel].sprites;
        }

        Debug.Log("Aiming Tower Shoot Lvl Up for all tower");

        gears.GetComponent<Gears>().playerI.GetComponent<Player>().SetState(new PlayerBuildingState(
            gears.GetComponent<Gears>().playerI.GetComponent<Player>(), myStats.constructionTime));
        
        if (towerLevel + 1 == maxLevel)
        {
            levelUpButton.GetComponent<Image>().sprite = shadedLvlUpButtonSprite;
            
            goldForNextLevelText.text = "";
        }
        
        
        AudioSource.PlayClipAtPoint(construction, new Vector3(transform.position.x,transform.position.y,-8), volume);
        SetBuildingState(this, currentState, myStats.sprite);
    }
}

[System.Serializable]
public class AimingSpritePerLvl
{
    public Sprite[] sprites;
}
