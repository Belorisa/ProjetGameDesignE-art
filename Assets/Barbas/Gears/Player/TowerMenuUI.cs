using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuUI : QuetionMarkUI
{
    public Button myButton;

    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        _effectDescription.text = _description;

        myButton.onClick.AddListener(WhenClicked);
    }
    
    void Update()
    {
        //myTextButton.color = new Color(myTextButton.color.r, myTextButton.color.g, myTextButton.color.b, myButton.colors.normalColor.a);

        if (selected)
        {
            myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * posOnScreenWidth, Screen.height * posOnScreenHeight, 0);
        }
    }

    public virtual void WhenClicked()
    {
        gears.GetComponent<Gears>().playerI.GetComponent<Player>().TowerMenu();
    }
}
