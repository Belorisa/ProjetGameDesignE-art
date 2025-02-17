using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmmunitionDeathRattle : DeathEffect
{
    private Ennemis me;
    
    private int ammunition;

    private GameObject ammunitionPack;
    
    public DropAmmunitionDeathRattle(Ennemis ennemis, int nbrAmmunition, GameObject ammunitionPack) : base(ennemis)
    {
        me = ennemis;
        
        ammunition = nbrAmmunition;

        this.ammunitionPack = ammunitionPack;
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

    public override void DeathRattle()
    {
        me.gears.GetComponent<Gears>().eventManager.GetComponent<EventManager>().DropAmmunition(stateMachine.gameObject, ammunition);
    }
}
