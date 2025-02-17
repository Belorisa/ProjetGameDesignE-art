using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateTurret : StateTower
{
    private Player me;
    
    public Vector2 mouseOnTheWorld;

    public int damages;

    public float shootTimer, fireRate;

    public GameObject bullet;

    public Vector2 canonPos;

    public float bulletSpeed;

    private int _currentAiming;
    
    public PlayerStateTurret(Player me, int damages, float fireRate, GameObject bullet, Vector2 canonPos, float bulletSpeed) : base(me)
    {
        this.me = me;

        this.damages = damages;
        this.fireRate = fireRate;

        this.bullet = bullet;

        this.canonPos = canonPos;
        this.bulletSpeed = bulletSpeed;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Player State : turret");
        
        //me.GetComponent<SpriteRenderer>().color = Color.red;

        if (me.towerMenuShow)
        {
            me.TowerMenu();
        }
    }
    public override void Tick()
    {
        shootTimer -= Time.deltaTime;
            
        shootTimer = Mathf.Clamp(shootTimer, 0, fireRate);
        
        Vector3 relative = me.transform.InverseTransformPoint(me.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition));
        float _angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;

        if (_angle < 180 && _angle > 157.5f || _angle > -180 && _angle < -157.5f)
        {
            UpdateAiming(0);
        }
        else if (_angle < 157.5f && _angle >= 112.5f)
        {
            UpdateAiming(1);
        }
        else if (_angle < 112.5f && _angle >= 67.5f)
        {
            UpdateAiming(2);
        }
        else if (_angle < 67.5f && _angle >= 22.5f)
        {
            UpdateAiming(3);
        }
        else if (_angle < 22.5f && _angle >= -22.5f)
        {
            UpdateAiming(4);
        }
        else if (_angle < -22.5 && _angle >= -67.5f)
        {
            UpdateAiming(5);
        }
        else if (_angle < -67.5f && _angle > -112.5f)
        {
            UpdateAiming(6);
        }
        else if (_angle < -112.5f && _angle > -157.5f)
        {
            UpdateAiming(7);
        }
        
        mouseOnTheWorld = me.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition);
        
        //Vector2 dir = mouseOnTheWorld - new Vector2(me.transform.position.x, me.transform.position.y);
        
        Vector3 dir = new Vector3(mouseOnTheWorld.x, mouseOnTheWorld.y, 0) - me.transform.position;

        if (Input.GetButton("Fire1") && shootTimer <= 0 && me.ammunition > 0 && !Gears.gears.eventManager.mouseOnUi)
        {
            float distance = dir.magnitude;
            Vector2 direction = dir / distance;
            direction.Normalize();
            
            float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            GameObject bull = me.FuncInstantiate(bullet, canonPos, bullet.transform);
            
            bull.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            
            bull.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            

            bull.GetComponent<Bullet>().myStats = new BulletStats(damages, bulletSpeed, new PlayerTurretBullet(bull.GetComponent<Bullet>()), null);
            //refaire un state pour faire des buff

            me.ammunition--;
            
            me.gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().ammunitionIndicator.UpdateAmmunition();
            
            shootTimer = fireRate;
        }//shoot
    }
    
    public void UpdateAiming(int i)
    {
        if (_currentAiming != i)
        {
            me.playerR.GetComponent<SpriteRenderer>().sprite = me.spritesTurretMode[i];
            //firePoint.localPosition = localPosFirePoint[i];
        }

        _currentAiming = i;
    }

    public override void OnStateExit()
    {
        //me.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
