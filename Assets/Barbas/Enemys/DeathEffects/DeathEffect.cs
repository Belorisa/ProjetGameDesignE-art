using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : StateTower
{
    public DeathEffect(Ennemis ennemis) : base(ennemis)
    {
        
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

    public virtual void DeathRattle()
    {
        Debug.Log("Base Death Rattle");
    }
}
