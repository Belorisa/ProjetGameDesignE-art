using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoomRangV2 : TowerShoot
{
    public Sprite idleSprite;

    public Sprite shootingSprite;

    public Sprite boomRangSprite;

    public AimingSpritePerLvl[] my2SpritesPerLvl;

    private LineRenderer _lineRenderer;

    private GameObject mainBullet;

    //public bool shooting;
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        _lineRenderer = GetComponent<LineRenderer>();
        
        _lineRenderer.SetPosition(0, transform.position);
        
        _lineRenderer.SetPosition(1, transform.position);

        StartForEveryOne(this, new ShootingTowersChangingSprite(this, idleSprite, shootingSprite)); //(this, null);
        
        targetingSysteme = new TargetAdvancedEnemy(this);
        
        bulletMovments = new BulletBoomRangeMovments(this);
        
        bulletEffect = new BulletBoomRangEffect(this);
    }
    
    new void Update()
    {
        currentState.Tick();

        ShowTowerInfos();

        if (mainBullet != null)
        {
            _lineRenderer.SetPosition(1, new Vector3(mainBullet.transform.position.x, mainBullet.transform.position.y, mainBullet.transform.position.z + 0.5f));
        }

        //Debug.Log("TowerBoomRang - " + gameObject.name);
    }
    
    public override void TowerShootSystem()
    {
        //Debug.Log("BoomRang Shoot");
        
        GameObject baseBullet = InstantiateBullet();

        baseBullet.GetComponent<SpriteRenderer>().sprite = boomRangSprite;

        baseBullet.GetComponent<BulletV2>().myStats = new BulletStatsV2(myStats.damages, myStats.bulletSpeed,
            bulletMovments.ShallowCopy(), targetingSysteme.target, bulletEffect.ShallowCopy());

        baseBullet.GetComponent<BulletV2>().myStats.movments.me = baseBullet.GetComponent<BulletV2>();
        
        baseBullet.GetComponent<BulletV2>().myStats.effect.me = baseBullet.GetComponent<BulletV2>();

        //baseBullet.GetComponent<BulletV2>().myStats.effect.explosion = shootFeedBack;

        mainBullet = baseBullet;
    }
    
    public override void SetBuildingState(TowerV2 tower, StateTower towerBeh, Sprite sprite)
    {
        SetState(new BuildingStateTowerShoot(this, new ShootingTowersChangingSprite(this, idleSprite, shootingSprite), myStats.constructionTime, sprite));
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

                idleSprite = my2SpritesPerLvl[towerLevel].sprites[0];
                
                shootingSprite = my2SpritesPerLvl[towerLevel].sprites[1];

                boomRangSprite = my2SpritesPerLvl[towerLevel].sprites[2];
            
                LevelForAllTower();
            
                Debug.Log("Level up made : Tower BoomRang");
                towerLevel++; //sa doit être à la fin
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}
