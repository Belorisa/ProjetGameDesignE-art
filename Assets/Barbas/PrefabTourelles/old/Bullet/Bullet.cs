using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : StateMachine
{
    public BulletStats myStats;

    public Vector2 point;// = new Vector2(18, 7);
    public Vector2 point2;// = new Vector2(-10, -14);
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        point = gears.GetComponent<Gears>().cam.GetComponent<CameraScript>().maxCameraPos + new Vector3(10, 10);
        
        point2 = gears.GetComponent<Gears>().cam.GetComponent<CameraScript>().minCameraPos + new Vector3(-10, -10);
        
        SetState(myStats.behaviour);
        
        myStats.effect?.OnStateEnter();
    }
    
    new void Update()
    {
        currentState.Tick();
        
        myStats.effect?.Tick(); //? = null propagation = check si c null
        
        if (transform.position.x >= point.x || transform.position.x <= point2.x || transform.position.y >= point.y || transform.position.y <= point2.y) //détruire si trop loing
        {
            DestroyObject(gameObject);
        }
    }
    
    /*public void SetEffect(StateTower state)
    {
        if (myStats.effect != null)
            myStats.effect.OnStateExit();

        myStats.effect = state;

        if (myStats.effect != null)
            myStats.effect.OnStateEnter();
    }*/
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("ColFromBullet");
        if (col.gameObject.CompareTag("Enemy"))
        {
            currentState?.OnCollision(col);
        }
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("ColFromBullet");
        if (col.gameObject.CompareTag("Enemy"))
        {
            currentState?.OnCollision(col);
        }
    }
}

[System.Serializable]
public class BulletStats
{
    public int damage;
    public float speed;

    public StateTower behaviour;
    public StateTower effect;
    public GameObject target;

    public BulletStats(int dmg, float spee, StateTower beh, GameObject targ)
    {
        damage = dmg;
        speed = spee;

        behaviour = beh;
        target = targ;
    }
    
    public BulletStats(int dmg, float spee, StateTower beh, GameObject targ, StateTower eff)
    {
        damage = dmg;
        speed = spee;

        behaviour = beh;
        effect = eff;
        target = targ;
    }
}