using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipUITimer : QuetionMarkUI
{
    public float timer;
    
    private float _timer;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        _effectDescription.text = _description;

        _timer = timer;
    }
    
    void Update()
    {
        if (selected)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                myTips.SetActive(true);
                
                myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * posOnScreenWidth, Screen.height * posOnScreenHeight, 0);
            }
        }
    }
    
    public override void OnHighlightEnter()
    {
        selected = true;
    }
    
    public override void OnHighlightExit()
    {
        _timer = timer;
            
        selected = false;
        
        myTips.SetActive(false);
    }
}
