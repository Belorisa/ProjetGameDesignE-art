using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using Tile = UnityEngine.Tilemaps.Tile;

public class Player : StateMachine
{
    public Animator animator;

    public Sprite[] spritesTurretMode;
    
    public GameObject feets;

    public Vector3 _startScale;

    public Rigidbody2D rb2d;
    
    public float actionRange;
    
    public float maxSpeed;
    public float speed;
    public float startSpeed;

    public float acceleration;

    public int turretDamage;
    public float turretFireRate;
    public float turretBulletSpeed;
    public GameObject canonPos;
    public GameObject bullet;

    public float maxAmmunition = 100;
    public float ammunition = 50;

    public GameObject rangeToolTip;

    public GameObject playerR;
    
    public GameObject towerMenu;
    public bool towerMenuShow;

    public float maxX, maxY;

    public float magnetAmmPackRange;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        _startScale = transform.localScale;

        rb2d = GetComponent<Rigidbody2D>();
        
        towerMenu.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);

        gears.GetComponent<Gears>().playerI = gameObject;
        
        SetState(new PlayerStateMoov(this));
    }

    void FixedUpdate()
    {
        if (!gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().isPaused)
        {
            currentState?.Tick();
        }
    }
    
    new void Update()
    {
        /*if (currentState == null)
        {
            SetState(new PlayerStateMoov(this));
        }*/

        if (Input.GetButtonDown("Left Shift"))
        {
            TowerMenu();
        }

        if (Input.GetButtonDown("Fire2") && towerMenuShow)
        {
            TowerMenu();
        }

        if (currentState?.GetType().Name == typeof(PlayerStateTurret).Name && Input.GetButtonDown("Fire2"))
        {
            TurretMode();
        }

        if (Input.GetButtonDown("Jump"))
        {
            TurretMode();
        }
        
        //sortir le joueur de la route
        if (Gears.gears.pathManager.GetComponent<WaveManagerB>().isRunning && gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(new Vector2(feets.transform.position.x, feets.transform.position.y))) != null) //sortir le joueur de la route
        {
             int i1 = 1;

            var t1 = gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(new Vector2(feets.transform.position.x, feets.transform.position.y)));

            while (t1 != null)
            {
                t1 = (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(i1,0))));
                
                i1++;
            }
            
            int i2 = 1;

            var t2 = gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(feets.transform.position.x, feets.transform.position.y)));

            while (t2 != null)
            {
                t2 = (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(transform.position.x, transform.position.y) + new Vector2(-i2,0))));
                
                i2++;
            }
            
            int i3 = 1;

            var t3 = gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(feets.transform.position.x, feets.transform.position.y)));

            while (t3 != null)
            {
                t3 = (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0,i3))));
                
                i3++;
            }
            
            int i4 = 1;

            var t4 = gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(feets.transform.position.x, feets.transform.position.y)));

            while (t4 != null)
            {
                t4 = (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0,-i4))));
                
                i4++;
            }
            
            //Debug.Log("Droite : " + i1 + "   Gauche : " + i2 + "   Haut : " + i3 + "   Bas : " + i4);
            if (i1 != i2 && i3 != i4 && Mathf.Min(i1, i2) != Mathf.Min(i3, i4))
            {
                if (Mathf.Min(Mathf.Min(i1, i2), Mathf.Min(i3, i4)) == i1)
                {
                    //Debug.Log("droite");
                
                    transform.Translate(new Vector2(1,0));
                }else
                if (Mathf.Min(Mathf.Min(i1, i2), Mathf.Min(i3, i4)) == i2)
                {
                    //Debug.Log("gauche");
                
                    transform.Translate(new Vector2(-1,0));
                }else
                if (Mathf.Min(Mathf.Min(i1, i2), Mathf.Min(i3, i4)) == i3)
                {
                    //Debug.Log("Haut");
                
                    transform.Translate(new Vector2(0,1));
                }else
                if (Mathf.Min(Mathf.Min(i1, i2), Mathf.Min(i3, i4)) == i4)
                {
                    //Debug.Log("bas");
                
                    transform.Translate(new Vector2(0,-1));
                }
            }
            else
            {
                float minDistance = 0;

                Vector2 dir = Vector2.zero;

                if (Vector2.Distance(new Vector2(gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(i1, 0)).x, 
                        gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(i1, 0)).y), transform.position) < minDistance)
                {
                    dir = new Vector2(i1, 0);
                }
                
                if (Vector2.Distance(new Vector2(gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(-i2, 0)).x, 
                        gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(-i2, 0)).y), transform.position) < minDistance)
                {
                    dir = new Vector2(-i2, 0);
                }
                
                if (Vector2.Distance(new Vector2(gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0, i3)).x, 
                        gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0, i3)).y), transform.position) < minDistance)
                {
                    dir = new Vector2(0, i3);
                }
                
                if (Vector2.Distance(new Vector2(gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0, -i4)).x, 
                        gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                            new Vector2(feets.transform.position.x, feets.transform.position.y) + new Vector2(0, -i4)).y), transform.position) < minDistance)
                {
                    dir = new Vector2(0, -i4);
                }
                
                transform.Translate(dir);
            }
        }
    }

    void LateUpdate()
    {
        maxX = transform.position.x;
        maxY = transform.position.y;
        
        var point = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(gears.GetComponent<Gears>().cam.pixelWidth, 
            gears.GetComponent<Gears>().cam.pixelHeight));
        
        var point2 = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(0, 0));

        transform.position = new Vector3(Mathf.Clamp(maxX, point2.x, point.x), 
            Mathf.Clamp(maxY, point2.y, point.y), transform.position.z);
    } //bloqué le joueur dans la camera

    public void ShowRange()
    {
        if (!rangeToolTip.activeSelf)
        {
            rangeToolTip.SetActive(true);
        }
        else
        {
            rangeToolTip.SetActive(false);
        }
        rangeToolTip.transform.localScale =
            new Vector3(actionRange * 2, actionRange * 2, rangeToolTip.transform.localScale.z);
    }

    public IEnumerator ShowRangeTimer()
    {
        ShowRange();
        yield return new WaitForSeconds(0.5f);
        ShowRange();
    }

    public void TowerMenu()
    {
        if (!gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().isPaused)
        {
            if (!towerMenuShow && currentState.GetType().Name == typeof(PlayerStateMoov).Name)
            {
                towerMenu.transform.position = gears.GetComponent<Gears>().cam.WorldToScreenPoint(transform.position);
                
                towerMenu.SetActive(true);
            }
            else
            {
                for (int i = 0; i < towerMenu.transform.childCount; i++)
                {
                    if (towerMenu.transform.GetChild(i).GetComponent<TowerMenuUI>() != null)
                    {
                        towerMenu.transform.GetChild(i).GetComponent<TowerMenuUI>().myTips.SetActive(false);
                    }
                }
            
                towerMenu.SetActive(false);
            }

            towerMenuShow = !towerMenuShow;
        }
    }

    public void TurretMode()
    {
        if (!gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().isPaused 
                                        && (currentState?.GetType().Name == typeof(PlayerStateTurret).Name || currentState?.GetType().Name == typeof(PlayerStateMoov).Name))
        {
            if (currentState.GetType().Name == typeof(PlayerStateTurret).Name)
            {
                gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().InterfaceSwitch();
                
                StartCoroutine(ExitFromTurretMode(new PlayerStateMoov(this)));
            }
            else if (gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                         gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(feets.transform.position)) == null)
            {
                gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().InterfaceSwitch();
                
                gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().SetState(null); //annuler toutes les action
                
                StartCoroutine(DeployTurretMode());
            }
        }
    }
    
    public IEnumerator DeployTurretMode()
    {
        animator.SetBool("IsWalking", false);
        
        animator.SetBool("SwitchOnTurret", true);

        SetState(null);

        yield return new WaitForSeconds(0.6f);
        
        animator.SetBool("SwitchOnTurret", false);
        
        animator.enabled = false;
        
        SetState(new PlayerStateTurret(this, turretDamage, turretFireRate, bullet, canonPos.transform.position, turretBulletSpeed));
    }
    
    public IEnumerator ExitFromTurretMode(StateTower nextState)
    {
        animator.enabled = true;
        
        animator.SetBool("IsWalking", false);
        
        animator.SetBool("SwitchOnTurret", false);
        
        animator.SetBool("SwitchOffTurret", true);
        
        yield return new WaitForSeconds(0.8f);

        animator.SetBool("SwitchOffTurret", false);
        
        SetState(nextState);
    }
}
