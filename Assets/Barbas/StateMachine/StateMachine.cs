using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public StateTower currentState;

    public GameObject gears;

    private string s;

    public void Awake()
    {
        s = gameObject.name;
    }
    
    public void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }

    public void Update()
    {
        currentState.Tick();
    }

    public void SetState(StateTower state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        if (currentState != null)
        {
            gameObject.name = s + " - " + state.GetType().Name;
        }

        if (currentState != null)
            currentState.OnStateEnter();
    }
    
    public GameObject FuncInstantiate(GameObject go, Vector2 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }
    
    public GameObject FuncInstantiate(GameObject go, Vector3 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }

    public void DestroyObject(GameObject go)
    {
        Destroy(go);
    }
    
    /*public void SetState2(StateTower state, StateTower t)
    {
        if (t != null)
            t.OnStateExit();

        t = state;
        if (t != null)
        {
            gameObject.name = s + " - " + state.GetType().Name;
        }

        if (t != null)
            t.OnStateEnter();
    }*/
}