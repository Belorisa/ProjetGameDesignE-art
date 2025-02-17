using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletV2 : StateMachine
{
    public BulletStatsV2 myStats;

    public Vector2 point; //= new Vector2(18, 7);
    public Vector2 point2;// = new Vector2(-10, -14);

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        point = gears.GetComponent<Gears>().cam.GetComponent<CameraScript>().maxCameraPos + new Vector3(20, 20);
        
        point2 = gears.GetComponent<Gears>().cam.GetComponent<CameraScript>().minCameraPos + new Vector3(-20, -100);

        SetState(myStats.movments);

        myStats.effect?.OnStateEnter();
    }
    
    new void Update()
    {
        currentState?.Tick();
        
        myStats.effect?.Tick(); //? = null propagation = check si c null
        
        if (transform.position.x >= point.x || transform.position.x <= point2.x || transform.position.y >= point.y || transform.position.y <= point2.y) //détruire si trop loing
        {
            Debug.Log("Out of cam - " + gameObject.name);
            
            //Debug.Log(transform.position + " max pos : " + point + " min Pos : " + point2);
            
            OnOutOfCam();
            
            DestroyObject(gameObject);
        }
    }

    public virtual void OnOutOfCam()
    {
        
    }
    
    /*public void SetEffect(StateTower state)
    {
        if (myStats.effect != null)
            myStats.effect.OnStateExit();

        myStats.effect = state;

        if (myStats.effect != null)
            myStats.effect.OnStateEnter();
    }*/
}

[System.Serializable]
public class BulletStatsV2
{
    public int damage;
    public float speed;

    public BulletMovments movments;
    public BulletMovments effect;
    
    public GameObject target;

    public BulletStatsV2(int dmg, float spee, BulletMovments beh, GameObject targ)
    {
        damage = dmg;
        speed = spee;

        movments = beh;
        target = targ;
    }
    
    public BulletStatsV2(int dmg, float spee, BulletMovments beh, GameObject targ, BulletMovments eff)
    {
        damage = dmg;
        speed = spee;

        movments = beh;
        effect = eff;
        target = targ;
    }
}