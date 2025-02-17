using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : StateTower
{
    public float stunTime;

    public StateTower comeBackTo;
    
    public StunnedState(StateMachine stateMachine, float timer, StateTower comeBackTo) : base(stateMachine)
    {
        stunTime = timer;
        this.comeBackTo = comeBackTo;
    }

    public override void OnStateEnter()
    {
       Debug.Log("stunned");
    }
    public override void Tick()
    {
        stunTime -= Time.deltaTime;

        if (stunTime <= 0)
        {
            stateMachine.SetState(comeBackTo);
        }
    }
    
    public override void OnStateExit()
    {
    }
}
