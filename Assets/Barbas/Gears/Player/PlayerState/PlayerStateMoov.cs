using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMoov : StateTower
{
    private Player me;
    public PlayerStateMoov(Player player) : base(player)
    {
        me = player;
    }
    
    public override void OnStateEnter()
    {
        //Debug.Log("Player State : Move");
    }
    public override void Tick()
    {
       newMove();
       
       if (me.towerMenuShow)
       {
           me.towerMenu.GetComponent<RectTransform>().position =
               me.gears.GetComponent<Gears>().cam.WorldToScreenPoint(me.transform.position);
       }
    }

    public void newMove() // V2 faite par Damione
    {
        
        me.speed += Time.deltaTime * me.acceleration;
        me.speed = Mathf.Clamp(me.speed, me.startSpeed, me.maxSpeed);
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 d = new Vector3( h, v, me.transform.position.z);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            me.animator.SetBool("IsWalking", true);
            
            //me.gameObject.transform.Translate(d.normalized*me.speed * Time.timeScale * Time.deltaTime * 70);
            me.rb2d.MovePosition(me.transform.position + d.normalized * Time.fixedDeltaTime * me.speed * 70);

            if (h > 0)
            {
                me.transform.localScale = new Vector3(-me._startScale.x, me._startScale.y, me._startScale.z);
            }
            
            if (h < 0)
            {
                me.transform.localScale = new Vector3(me._startScale.x, me._startScale.y, me._startScale.z);
            }
        }


        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            me.animator.SetBool("IsWalking", false);
            
            me.speed = me.startSpeed;
        }

        /*if (Input.GetKey(Gears.gears.up))
        {
            Debug.Log("z");
        }*/
    }
    public void OldMove()
    {
        if (Input.GetButton("LeftArrow"))
        {
            me.gameObject.transform.Translate(Vector2.left * me.speed * Time.timeScale * Time.deltaTime * 70);
            me.speed += Time.deltaTime * me.acceleration;
        }

        if (Input.GetButton("RightArrow"))
        {
            me.gameObject.transform.Translate(Vector2.right * me.speed * Time.timeScale * Time.deltaTime * 70);
            me.speed += Time.deltaTime * me.acceleration;
        }

        if (Input.GetButton("UpArrow"))
        {
            me.gameObject.transform.Translate(Vector2.up * me.speed * Time.timeScale * Time.deltaTime * 70);
            me.speed += Time.deltaTime * me.acceleration;
        }

        if (Input.GetButton("DownArrow"))
        {
            me.gameObject.transform.Translate(Vector2.down * me.speed * Time.timeScale * Time.deltaTime * 70);
            me.speed += Time.deltaTime * me.acceleration;
        }
        me.speed = Mathf.Clamp(me.speed, me.startSpeed, me.maxSpeed);

        if (!Input.GetButton("LeftArrow") && !Input.GetButton("RightArrow") && !Input.GetButton("UpArrow") &&
            !Input.GetButton("DownArrow"))
        {
            me.speed = me.startSpeed;
        }
    } // première version faite par Yoan 
    
    public override void OnStateExit()
    {
    }
}
