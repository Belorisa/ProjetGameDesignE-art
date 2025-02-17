using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParabollaV2 : TowerShoot
{
    public Animator animator;

    private GameObject baseBullet;
    
    public IEnumerator shootCoroutine;

    public AudioClip shootSound;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, new ShootingTowers(this)); //(this, null);
        
        targetingSysteme = new TargetAdvancedEnemy(this);
        
        bulletMovments = new ParabollaBulletMovments(this);

        bulletEffect = bulletEffect = new ParabollaBulletEffect(this);;

        shootCoroutine = Shoot();
    }

    public override void TowerShootSystem()
    {
        DebugedShoot();
    }

    public IEnumerator Shoot()
    {
        animator.SetBool("Shooting", true);
        
        yield return new WaitForSeconds(0.55f);
        
        GameObject baseBullet = InstantiateBullet();

        baseBullet.GetComponent<BulletV2>().myStats = new BulletStatsV2(myStats.damages, myStats.bulletSpeed,
            bulletMovments.ShallowCopy(), targetingSysteme.target, bulletEffect.ShallowCopy());

        baseBullet.GetComponent<BulletV2>().myStats.movments.me = baseBullet.GetComponent<BulletV2>();
        
        baseBullet.GetComponent<BulletV2>().myStats.effect.me = baseBullet.GetComponent<BulletV2>();
        
        animator.SetBool("Shooting", false);
    }
    
    public void DebugedShoot()
    {
        AudioSource.PlayClipAtPoint(shootSound, new Vector3(transform.position.x,transform.position.y,-8), volume);
        StopCoroutine(shootCoroutine);
        shootCoroutine = Shoot();
        StartCoroutine(shootCoroutine);
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
            
                Debug.Log("Level up made : Tower Parabolla");
                towerLevel++; //sa doit être à la fin
                
                animator.SetInteger("Lvl", towerLevel);
            }
            else
            {
                DebugedShowNotEnoughGolds();
            }
        }
    }
}
