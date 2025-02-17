using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerMenuUIBridges : TowerMenuUI
{
    public TextMeshProUGUI myTextButton;
    
    public TextMeshProUGUI bridgesNumberLeft;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        _effectDescription.text = _description;

        myButton.onClick.AddListener(WhenClicked);
        
        UpdateBridgesNumberText(gears.GetComponent<Gears>().playerActionManager.bridgesLeft);
    }
    
    void Update()
    {
        if (selected)
        {
            myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * posOnScreenWidth, Screen.height * posOnScreenHeight, 0);
        }
        
        if (!CanPlaceBridge())
        {
            myTextButton.color = Color.red;
            
            bridgesNumberLeft.color = Color.red;
        }
        else
        {
            myTextButton.color = Color.white;
            
            bridgesNumberLeft.color = Color.white;
        }
    }

    private bool CanPlaceBridge()
    {
        if (gears.GetComponent<Gears>().playerActionManager.GetComponent<PlayerActionManager>().bridgesLeft <= 0)
        {
            return false;
        }
        
        return true;
    }

    public void UpdateBridgesNumberText(int i)
    {
        bridgesNumberLeft.text = "Left : " + i;
    }
}
