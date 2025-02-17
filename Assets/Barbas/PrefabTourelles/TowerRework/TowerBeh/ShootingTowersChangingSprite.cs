using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTowersChangingSprite : ShootingTowers
{
    private Sprite _sprite1;

    private Sprite _sprite2;
    
    public ShootingTowersChangingSprite(TowerShoot tower, Sprite idleSprite, Sprite shootingSprite) : base(tower)
    {
        this.tower = tower;

        _sprite1 = idleSprite;

        _sprite2 = shootingSprite;
    }
    public override void Tick()
    {
        tower.targetingSysteme.Tick();

        if (tower.targetingSysteme.target != null)
        {
            if (fireTimer <= 0)
            {
                if (tower.towerR.GetComponent<SpriteRenderer>().sprite != _sprite2)
                {
                    tower.towerR.GetComponent<SpriteRenderer>().sprite = _sprite2;
                }

                tower.TowerShootSystem();
                
                //tower.mySounds.PlaySound("Shoot");
                
                fireTimer = tower.myStats.fireRate;
            }
        }

        fireTimer -= Time.deltaTime;
        fireTimer = Mathf.Clamp(fireTimer, 0, 100);

        if (fireTimer <= 0)
        {
            tower.towerR.GetComponent<SpriteRenderer>().sprite = _sprite1;
        }
    }
}
