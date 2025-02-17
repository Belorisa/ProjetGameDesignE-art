using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiState : EnemysState
{
    new public Ennemis me;
    public EnemiState(Ennemis ennemis) : base(ennemis)
    {
        me = ennemis;
    }

    public override void OnStateEnter()
    {
        
    }
    public override void Tick()
    {
        
    }
    
    public override void OnStateExit()
    {
    }

    public override void TakeDamages(int damages)
    {
        me.myStats.hp -= damages;
        //resistance
    }
}
