using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerV2 : StateMachine
{
    public int id;
    
    public int towerLevel; //dans l'affichage faire +1

    public int maxLevel;

    public Transform firePoint;

    public GameObject bullet;
    
    public TowerStats myStats;
    
    public TowerStats[] statsForEachLevel;

    public SoundManager soundManager;
    
    public float totalGoldSpentOnThisTower;
    
    //affichage
    public QuetionMarkUI lvlButtonUi;

    public GameObject uiTowerElements;
    
    public TextMeshProUGUI goldForNextLevelText;

    public GameObject lvlUpAura;
    
    public GameObject sellButton;

    public GameObject sellInfos;
    
    public GameObject levelUpButton;

    public GameObject lvlText;

    public float height;
    public float width;

    public GameObject notEnoughGold;

    public GameObject rangeCircle;

    public GameObject towerR;

    private Vector3 _startScaleTowerR;

    public GameObject buildingProgress;

    public IEnumerator showNotEnoughGoldCoroutine;

    private bool _mouseOver;

    private bool _hideTowerInfos;

    public Sprite shadedLvlUpButtonSprite;

    public AudioClip sellSound;

    public AudioClip construction;
    
    public AudioClip finishSound;

    public float volume;
    //private bool showInfos;

    new void Awake()
    {
        _startScaleTowerR = towerR.transform.localScale;
        
        //Debug.Log(transform.position.z);
    }
    
    new void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        StartForEveryOne(this, null); //(this, null);
    }     
    
    new void Update()
    {
        currentState.Tick();

        ShowTowerInfos();
        
        //Debug.Log("BaseUpdate - " + gameObject.name);
    }

    public virtual void SetBuildingState(TowerV2 tower, StateTower towerBeh) //base
    {
        SetState(new BuildingStateV2(tower, towerBeh, myStats.constructionTime));
    }
    
    public virtual void SetBuildingState(TowerV2 tower, StateTower towerBeh, Sprite sprite) //Lvl up
    {
        SetState(new BuildingStateV2(tower, towerBeh, myStats.constructionTime, sprite));
    }

    public void StartForEveryOne(TowerV2 me, StateTower towerBeh)
    {
        SetBuildingState(me, towerBeh);

        width = 1.8f;
        height = 1.8f;
        
        //Debug.Log(Screen.height + ": " + height2);

        sellButton.transform.SetParent(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().towerUiContainer.transform, false);
        sellButton.GetComponent<Button>().onClick.AddListener(SellTower);

        levelUpButton.transform.SetParent(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().towerUiContainer.transform, false);
        levelUpButton.GetComponent<Button>().onClick.AddListener(LevelUp);
        
        uiTowerElements.transform.SetParent(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().towerUiContainer.transform, false);
        notEnoughGold.transform.SetParent(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().towerUiContainer.transform, false);
        buildingProgress.transform.parent.transform.SetParent(gears.GetComponent<Gears>().uIManager.GetComponent<UIManager>().towerUiContainer.transform, false);

        showNotEnoughGoldCoroutine = ShowNotEnoughGold(0.7f);

        totalGoldSpentOnThisTower += myStats.goldCost;

        UpdateRange();
    }
    
    public GameObject InstantiateBullet()
    {
        //Debug.Log("Instantiate Bullet - " + gameObject.name);
        
        return Instantiate(bullet, firePoint.position, bullet.transform.rotation);
    }

    public void LevelUp()
    {
        Debug.Log("Ask for levelUp");
        
        if (gears.GetComponent<Gears>().playerI.GetComponent<Player>().currentState.GetType().Name == typeof(PlayerStateMoov).Name)
        {
            OnLevelUp(towerLevel);
            UpdateRange();
        }
    }

    public virtual void OnLevelUp(int i)
    {
        Debug.Log("Base Level up");
    }

    public virtual void LevelForAllTower()
    {
        OnHighlightExitLvlUpButton();

        gears.GetComponent<Gears>().playerI.GetComponent<Player>().SetState(new PlayerBuildingState(
            gears.GetComponent<Gears>().playerI.GetComponent<Player>(), myStats.constructionTime));

        if (towerLevel + 1 == maxLevel)
        {
            levelUpButton.GetComponent<Image>().sprite = shadedLvlUpButtonSprite;

            goldForNextLevelText.text = "";
        }
        AudioSource.PlayClipAtPoint(construction,new Vector3(transform.position.x,transform.position.y,-8), volume);

        SetBuildingState(this, currentState, myStats.sprite);
    }

    #region ButtonsFunc
    public virtual void OnHighlightEnterAnyButton()
    {
        gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().mouseOnUiElement = true;
    }
    
    public virtual void OnHighlightExitAnyButton()
    { 
        //Debug.Log("mouse on ui elemnt false");
        gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().mouseOnUiElement = false;
    }

    public void OnHighlightEnterSellButton()
    {
        OnHighlightEnterAnyButton();
        
        sellInfos.SetActive(true);

        sellInfos.GetComponent<TextMeshProUGUI>().text = "Sell for : " + totalGoldSpentOnThisTower * 0.75f;
    }

    public void OnHighlightExitSellButton()
    {
        OnHighlightExitAnyButton();
        
        sellInfos.SetActive(false);
    }

    public virtual void OnHighlightEnterLvlUpButton()
    {
        //Debug.Log("base lvlUp toolTip");
        
        lvlButtonUi.myTips.SetActive(true);
        
        if (towerLevel < maxLevel)
        {
            lvlButtonUi._description = "niveau suivant :" + " Degats : " + myStats.damages + " ->  " + statsForEachLevel[towerLevel].damages + '\n'
                                       + " Vitesse des balles : " + myStats.bulletSpeed + " ->  " + statsForEachLevel[towerLevel].bulletSpeed + '\n'
                                       + " Portee : " + myStats.range + " ->  " + statsForEachLevel[towerLevel].range + '\n'
                                       + " Candence de tire : " + myStats.fireRate + " ->  " + statsForEachLevel[towerLevel].fireRate + '\n'
                                       + "Temps de construction : " + statsForEachLevel[towerLevel].constructionTime;
        }
        else
        {
            lvlButtonUi._description = "Max level : " +  " Degats : " + myStats.damages + '\n'
                                       + '\n'
                                       + " Vitesse des balles : " + myStats.bulletSpeed + '\n'
                                       + '\n'
                                       + " Portee : " + myStats.range + '\n'
                                       + '\n'
                                       + " Candence de tire : " + myStats.fireRate + '\n'
                                       + '\n'
                                       + "Temps de construction : " + "0";
        }
        
        lvlButtonUi._effectDescription.text = lvlButtonUi._description;

        lvlButtonUi.selected = true;
        
        OnHighlightEnterAnyButton();
    }
    
    /*string ColorToHex(Color32 color) //changer la couleur d'un char
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }*/

    
    public virtual void OnHighlightExitLvlUpButton()
    {
        //Debug.Log("Base lvl Up button Exit");

        lvlButtonUi.myTips.SetActive(false);

        lvlButtonUi.selected = false;
        
        OnHighlightExitAnyButton();
    }
    
    #endregion 

    public void SellTower()
    {
        EventManager.current.SellTowerTriggerEnter(id); //trigger event sellTower
        
        currentState.OnStateExit();
        
        gears.GetComponent<Gears>().playerActionManager.golds += GetComponent<TowerV2>().totalGoldSpentOnThisTower * 0.75f;
        Debug.Log(gameObject.name + " sold for : " + GetComponent<TowerV2>().totalGoldSpentOnThisTower * 0.75f);
        
        gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().mouseOnUiElement = false;

        //Gears.gears.eventManager.PlaySound(soundManager.ReturnSound("SellTowerSound"), 2);
        
        //HideTowerInfos();
        AudioSource.PlayClipAtPoint(sellSound, transform.position, 0.5f);
        DestroyTower();
    }

    public void DestroyTower()
    {
        Destroy(uiTowerElements);
        
        Destroy(buildingProgress);
        
        Destroy(sellButton);
        
        Destroy(levelUpButton);
        
        Destroy(notEnoughGold);
        
        Destroy(gameObject);
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
        if (_mouseOver && towerLevel < maxLevel && gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds >= statsForEachLevel[towerLevel].goldCost
            || !_hideTowerInfos && towerLevel < maxLevel && 
            gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().golds >= statsForEachLevel[towerLevel].goldCost && Vector2.Distance(
                gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition), transform.position) < 2f)
        {
            lvlUpAura.SetActive(true);
        }
        else
        {
            lvlUpAura.SetActive(false);
        }
        
        if (lvlButtonUi.selected)
        {
            lvlButtonUi.myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * lvlButtonUi.posOnScreenWidth, 
                Screen.height * lvlButtonUi.posOnScreenHeight, 0);
        }
        
        if (Input.GetButtonDown("Fire1") && _mouseOver && !gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().mouseOnUiElement && 
            currentState.GetType().Name != typeof(BuildingStateV2).Name && !gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().isPaused)
        {
            Collider2D[] towerClose = Physics2D.OverlapCircleAll(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition), 2f, 
                gears.GetComponent<Gears>().TowerLayer);

            foreach (var tower in towerClose)
            {
                if (tower.gameObject != gameObject)
                {
                    tower.GetComponent<TowerV2>().HideTowerInfos();
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
        else  if (Vector2.Distance(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition),transform.position) > 2.8f ||
                  Vector2.Distance(gears.GetComponent<Gears>().playerI.transform.position, transform.position) >= 
                  gears.GetComponent<Gears>().playerI.GetComponent<Player>().actionRange)
        {
            HideTowerInfos();
        }

        if (Vector2.Distance(gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition),
                transform.position) < 2f) //update quand la souris n'est plus en range pour commencer a afficher
        {
            int i = towerLevel + 1;

            if (towerLevel != maxLevel)
            {
                lvlText.GetComponent<TextMeshProUGUI>().text = "Niveau : " + i;
                
                goldForNextLevelText.text = statsForEachLevel[towerLevel].goldCost.ToString();
                
                if (Gears.gears.playerActionManager.golds >= statsForEachLevel[towerLevel].goldCost)
                {
                    goldForNextLevelText.color = Color.white;
                }
                else
                {
                    goldForNextLevelText.color = Color.red;
                }
            }
            else
            {
                lvlText.GetComponent<TextMeshProUGUI>().text = "Max Level";
            }
            
            var point = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(gears.GetComponent<Gears>().cam.pixelWidth, 
                gears.GetComponent<Gears>().cam.pixelHeight));
        
            var point2 = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(0, 0));

            levelUpButton.transform.position =
                gears.GetComponent<Gears>().cam.WorldToScreenPoint(new Vector2(Mathf.Clamp(transform.position.x, point2.x, point.x), 
                    Mathf.Clamp(transform.position.y + 2.3f, point2.y, point.y)));
            
            uiTowerElements.transform.position =
                gears.GetComponent<Gears>().cam.WorldToScreenPoint(new Vector2(Mathf.Clamp(transform.position.x, point2.x, point.x),
                    Mathf.Clamp(transform.position.y + 2.3f, point2.y, point.y)));

            //lvlText.transform.position = Gears.gears.cam.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y) + new Vector2(1, 2.4f));
                //gears.GetComponent<Gears>().cam.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y) + CircleAdvance(0.35f) + new Vector2(Screen.width * 0.0001f, -Screen.height * 0.0001f));
            
                
            sellButton.transform.position = gears.GetComponent<Gears>().cam.WorldToScreenPoint(
                new Vector2(Mathf.Clamp(transform.position.x, point2.x, point.x), 
                    Mathf.Clamp(transform.position.y - 2, point2.y, point.y)));
        }
    }

    public void HideShowTowerInf() //pour caché le menu lvl up quand ont reclick
    {
        OnHighlightExitLvlUpButton();
        
        if (!levelUpButton.activeSelf && !rangeCircle.activeSelf)
        {
            uiTowerElements.SetActive(true);
            
            levelUpButton.SetActive(true);

            rangeCircle.SetActive(true);
            
            sellButton.SetActive(true);

            towerR.transform.localScale = new Vector3(_startScaleTowerR.x * 1.2f, _startScaleTowerR.y * 1.2f, _startScaleTowerR.z * 1.2f);

            _hideTowerInfos = false;

            //showInfos = true;
        }
        else
        {
            HideTowerInfos();
        }
    }

    public void HideTowerInfos()
    {
        if (!_hideTowerInfos)
        {
            //Debug.Log("Hide Tower Info - " + gameObject.name);
        
            //lvlUpAura.SetActive(false);
            
            uiTowerElements.SetActive(false);
        
            levelUpButton.SetActive(false);

            rangeCircle.SetActive(false);
        
            sellButton.SetActive(false);
        
            towerR.transform.localScale = _startScaleTowerR;

            OnHighlightExitAnyButton();

            //showInfos = false;

            _hideTowerInfos = true;
        }
    }

    public IEnumerator ShowNotEnoughGold(float time)
    {
        notEnoughGold.SetActive(true);

        notEnoughGold.transform.position = gears.GetComponent<Gears>().cam.WorldToScreenPoint(
                                               new Vector2(transform.position.x, transform.position.y) + CircleAdvance(0.15f) + new Vector2(2, 0));
        
        yield return new WaitForSeconds(time);
        
        notEnoughGold.SetActive(false);
    }

    public void DebugedShowNotEnoughGolds()
    {
        StopCoroutine(showNotEnoughGoldCoroutine);
        showNotEnoughGoldCoroutine = ShowNotEnoughGold(0.7f);
        StartCoroutine(showNotEnoughGoldCoroutine);
    }

    public Vector2 CircleAdvance(float progression)
    {
        return new Vector2(Mathf.Cos(2 * Mathf.PI * progression) * width, Mathf.Sin(2 * Mathf.PI * progression) * height);
    }
    
    void OnMouseOver()
    {
        _mouseOver = true;
    }
    
    void OnMouseExit()
    {
        _mouseOver = false;
    }
    
    void OnGUI()
    {
        /*if (showInfos)
        {
            if (GUI.Button(new Rect(gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[3].transform.position).x,
                gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[3].transform.position).y, 50, 30), "Sell"))
            {
                Debug.Log("Sell");
            }
            
            if (GUI.Button(new Rect(gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[0].transform.position).x,
                gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[0].transform.position).y, 60, 30), "LevelUp"))
            {
               LevelUp();
            }

            int i = towerLevel + 1;

            GUI.TextArea(new Rect(gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[1].transform.position).x,
                    gears.GetComponent<Gears>().cam.WorldToScreenPoint(showInfoHere[1].transform.position).y, 60, 20), "Level : " + i);
        }*/
    }
}
