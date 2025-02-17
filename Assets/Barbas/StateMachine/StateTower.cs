using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateTower
{
    protected StateMachine stateMachine;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public virtual void OnCollision(Collider2D col) { } //a mettre plus bas


    public StateTower(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}

/*public StunnedState(StateMachine stateMachine) : base(stateMachine)
{
        
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
}*/

/*public override void OnCollision(Collider2D col)
    {
        Debug.Log("col");
    }*/

