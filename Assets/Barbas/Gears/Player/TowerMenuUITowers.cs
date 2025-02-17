using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerMenuUITowers : TowerMenuUI
{
    private TowerV2 myTowerV2;
    
    public int towerIndex;
    
    public GameObject notEnoughGolds;
    
    public TextMeshProUGUI myTextButton;

    public TextMeshProUGUI energyCostText;
    
    private IEnumerator showNotEnoughGoldCoroutine;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        _effectDescription.text = _description;

        myButton.onClick.AddListener(WhenClicked);
        
        myTowerV2 = gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().towers[towerIndex].GetComponent<TowerV2>();
        
        showNotEnoughGoldCoroutine = ShowNotEnoughGold(0.5f);
    }
    
    void Update()
    {
        if (selected)
        {
            myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * posOnScreenWidth, Screen.height * posOnScreenHeight, 0);
        }
        
        if (!CanPlaceTower())
        {
            myTextButton.color = Color.red;

            energyCostText.color = Color.red;
        }
        else
        {
            myTextButton.color = Color.white;
            
            energyCostText.color = Color.white;
        }
    }
    
    public bool CanPlaceTower()
    {
        if (gears.GetComponent<Gears>().playerActionManager.golds < myTowerV2.myStats.goldCost)
        {
            return false;
        }
        
        return true;
    }
    
    public override void WhenClicked()
    {
        if (CanPlaceTower())
        {
            gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().PlaceTower(towerIndex);
            
            gears.GetComponent<Gears>().playerI.GetComponent<Player>().TowerMenu();
        }
        else
        {
            DebugedShowNotEnoughGolds(0.5f);
        }
    }
    
    public IEnumerator ShowNotEnoughGold(float time)
    {
        notEnoughGolds.SetActive(true);

        yield return new WaitForSeconds(time);
        
        notEnoughGolds.SetActive(false);
    }
    
    public void DebugedShowNotEnoughGolds(float t)
    {
        StopCoroutine(showNotEnoughGoldCoroutine);
        showNotEnoughGoldCoroutine = ShowNotEnoughGold(t);
        StartCoroutine(showNotEnoughGoldCoroutine);
    }
}
