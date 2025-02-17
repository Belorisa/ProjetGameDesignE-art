using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : StateMachine
{
    public StateTowerLevelUp mainState;
    
    public int towerLevel; //dans l'affichage faire +1

    public int maxLevel;

    public TowerStats myStats;
    
    public float constructionTime;

    public float totalGoldSpentOnThisTower;
    
    public Transform firePoint;

    public GameObject bullet;
    
    public TowerStats[] statsForEachLevel;
    
    //affichage
    public GameObject levelUpButton;

    public GameObject lvlText;

    public GameObject[] showInfoHere;

    public GameObject rangeCircle;

    public GameObject towerR;

    private Vector3 startScale;

    public GameObject buildingProgress;

    private bool mouseOver;

    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        SetState(mainState);
        
        StartForEveryOne(this, null);
    }     
    
    new void Update()
    {
        currentState.Tick();
        
        ShowTowerInfos();
    }

    public void StartForEveryOne(Tower me, StateTowerLevelUp towerBeh)
    {
        SetState(new BuildingState(me, towerBeh));
        
        levelUpButton.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);
        lvlText.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);
        buildingProgress.transform.parent.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);
        
        totalGoldSpentOnThisTower += myStats.goldCost;

        startScale = transform.localScale;
        
        UpdateRange();
    }
    
    public GameObject InstantiateBullet()
    {
        return Instantiate(bullet, firePoint.position, bullet.transform.rotation);
    }

    public void LevelUp()
    {
        mainState.OnLevelUp(towerLevel);
        UpdateRange();
        Debug.Log("Ask for levelUp");
    }

    public void UpdateRange()
    {
        if (rangeCircle)
        {
            rangeCircle.transform.localScale = new Vector3(myStats.range * 2, myStats.range * 2, rangeCircle.transform.localScale.z);
        }
    }

    public void ShowTowerInfos()
    {
        if (Input.GetButtonDown("Fire1") && mouseOver && mainState.GetType().Name != typeof(BuildingState).Name)
        {
            Collider2D[] towerClose = Physics2D.OverlapCircleAll(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition), 2f, 
                gears.GetComponent<Gears>().TowerLayer);

            foreach (var tower in towerClose)
            {
                if (tower.gameObject != gameObject)
                {
                    tower.GetComponent<Tower>().HideTowerInfos();
                }
            } //cacher le menu des autres tourelles
                
            if (Vector2.Distance(gears.GetComponent<Gears>().playerI.transform.position, transform.position) <= 
                gears.GetComponent<Gears>().playerI.GetComponent<Player>().actionRange)
            {
                HideShowTowerInf();
            }
            else
            {
                StartCoroutine(gears.GetComponent<Gears>().playerI.GetComponent<Player>().ShowRangeTimer()); //montrer la range du joueur
            }
        }
        else  if (Vector2.Distance(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition),transform.position) > 2f)
        {
            HideTowerInfos();
        }

        if (Vector2.Distance(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition),
                transform.position) < 2f) //update quand la souris n'est plus en range pour commencer a afficher
        {
            int i = towerLevel + 1;
                
            lvlText.GetComponent<TextMeshProUGUI>().text = "Lvl : " + i;
            
            levelUpButton.transform.position = gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[0].transform.position);
            
            lvlText.transform.position = gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[1].transform.position);
        }
    }

    public void HideShowTowerInf() //pour caché le menu lvl up quand ont reclick
    {
        if (!levelUpButton.activeSelf && !lvlText.activeSelf)
        {
            levelUpButton.SetActive(true);
            
            lvlText.SetActive(true);

            rangeCircle.SetActive(true);

            towerR.transform.localScale = new Vector3(startScale.x * 1.2f, startScale.y * 1.2f, startScale.z * 1.2f);
        }
        else
        {
            HideTowerInfos();
        }
    }

    public void HideTowerInfos()
    {
        levelUpButton.SetActive(false);
        lvlText.SetActive(false);
        rangeCircle.SetActive(false);
        towerR.transform.localScale = startScale;
    }
    
    void OnMouseOver()
    {
        mouseOver = true;
    }
    
    void OnMouseExit()
    {
        mouseOver = false;
    }
}

[System.Serializable]
public class TowerStats
{
    public int damages;

    public float bulletSpeed;

    public float range;

    public float fireRate;
    
    public float goldCost;

    public float constructionTime;

    public Sprite sprite;
} 
