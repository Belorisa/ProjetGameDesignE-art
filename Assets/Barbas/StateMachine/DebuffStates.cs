using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffStates : StateMachine
{
    public List<StateTower> debuffs = new List<StateTower>();
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }
    
    new void Update()
    {
        currentState.Tick();

        foreach (var debuff in debuffs)
        {
            debuff.Tick();
        }

        for (int i = debuffs.Count - 1; i >= 0; i--)
        {
            debuffs[i].Tick();
        }
    }

    public void SetDebuff(StateTower debuff)
    {
        debuff.OnStateEnter();
        
        debuffs.Add(debuff);
    }

    public void RemoveDebuff(StateTower debuff)
    {
        debuff.OnStateExit();

        for (int i = debuffs.Count - 1; i >= 0; i--)
        {
            if (debuffs[i] == debuff)
            {
                debuffs.RemoveAt(i);
            }
        }
    }
}
