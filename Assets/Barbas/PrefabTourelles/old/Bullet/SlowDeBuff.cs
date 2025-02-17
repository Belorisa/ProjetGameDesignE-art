using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDeBuff : StateTower
{
    public Ennemis enemyAffected;
    
    public float slowPower;

    public float slowTimer;
    
   public SlowDeBuff(Ennemis enemyAffected, float slowPower, float slowTimer) : base(enemyAffected)
   {
       this.enemyAffected = enemyAffected;

       this.slowPower = slowPower;

       this.slowTimer = slowTimer;
   }
   
   public override void OnStateEnter()
   {
       //Debug.Log("Slow Debuff");

       for (int i = enemyAffected.debuffs.Count - 1; i >= 0; i--)
       {
           if (enemyAffected.debuffs[i].GetType() == GetType())
           {
               enemyAffected.RemoveDebuff(enemyAffected.debuffs[i]);
           }
       }
       enemyAffected.myStats.speed -= slowPower;
   }
   public override void Tick()
   {
       slowTimer -= Time.deltaTime;

       if (slowTimer <= 0)
       {
           enemyAffected.RemoveDebuff(this);
       }
   }
       
   public override void OnStateExit()
   {
       enemyAffected.myStats.speed += slowPower;
   }
}
