using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : MonoBehaviour
{
    // Start is called before the first frame update
    
    private State currentState;

    
    public Rigidbody2D rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 0.2f ;
        SetState(new Idle(this));
    }

    private void Update()
    {
        currentState.Tick();
    }

    public void SetState(State state)
    {
        currentState = state;
    }
}

public abstract class State
{
    protected HeroState HeroState;

    public abstract void Tick();


    public State(HeroState heroState)
    {
        this.HeroState = heroState;
    }
}


public class Idle : State
{
    public Idle(HeroState heroState) : base(heroState){}
    public override void Tick()
    {
        Debug.Log("Idle");
        //HeroState.SetState(new Moving(HeroState));
        CheckState();
    }

    public void CheckState()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            HeroState.SetState(new Moving(HeroState));
        }

        if (Input.GetButton("Jump"))
        {
            HeroState.SetState(new Turret(HeroState));
        }

        if (Input.GetButton("Mouse ScrollWheel"))
        {
            HeroState.SetState(new Recharge(HeroState));
        }
    }

}

public class Moving : State
{
    public Moving(HeroState heroState) : base(heroState)
    {
    }

    public override void Tick()
    {
        Debug.Log("Moving");
        //HeroState.SetState(new Turret(HeroState));
        Move();
    }

    public void Move()
    {
        float moveHori = Input.GetAxis("Horizontal");
        float moveVerti = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHori , moveVerti);
        
        HeroState.rb.AddForce(movement * HeroState.speed);
    }

    public void Move1()
    {
       
    }
}

public class Turret : State
{
    public Turret(HeroState heroState) : base(heroState)
    {
        
    }

    public override void Tick()
    {
        Debug.Log("Turret");
        HeroState.SetState(new Recharge(HeroState));
    }
    
}

public class Recharge : State
{
    public Recharge(HeroState heroState) : base(heroState)
    {
        
    }

    public override void Tick()
    {
        Debug.Log("Recharge");
        HeroState.SetState(new Idle(HeroState));
    }
}