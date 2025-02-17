using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysState : StateTower
{
    public Ennemis me;
    
    public EnemysState(Ennemis ennemis) : base(ennemis)
    {
        me = ennemis;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("");
    }
    public override void Tick()
    {
        
    }
    
    public override void OnStateExit()
    {
    }

    public virtual void TakeDamages(int damages)
    {
        me.myStats.hp -= damages;
    }
}
