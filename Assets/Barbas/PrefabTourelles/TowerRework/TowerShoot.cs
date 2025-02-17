using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : TowerV2
{
   public Sound bulletSound;
   
   public TargetingSysteme targetingSysteme;

   public BulletMovments bulletMovments;

   public BulletMovments bulletEffect;

   public GameObject shootFeedBack;

   public virtual void TowerShootSystem()
   {
       //Debug.Log("Base Shoot");
       
       GameObject baseBullet = InstantiateBullet();

       baseBullet.GetComponent<BulletV2>().myStats = new BulletStatsV2(myStats.damages, myStats.bulletSpeed,
         bulletMovments.ShallowCopy(), targetingSysteme.target, bulletEffect.ShallowCopy());

       baseBullet.GetComponent<BulletV2>().myStats.movments.me = baseBullet.GetComponent<BulletV2>();
        
       baseBullet.GetComponent<BulletV2>().myStats.effect.me = baseBullet.GetComponent<BulletV2>();

       //baseBullet.GetComponent<BulletV2>().myStats.effect.explosion = shootFeedBack;
   }

   public override void SetBuildingState(TowerV2 tower, StateTower towerBeh, Sprite sprite)
   {
       SetState(new BuildingStateTowerShoot(this, towerBeh, myStats.constructionTime, sprite));
   }
}
