using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuetionMarkUI : MonoBehaviour
{
    public GameObject gears;
    
    public GameObject myTips;
    
    public TextMeshProUGUI _effectDescription;

    [TextArea(14,10)] public string _description;

    public float posOnScreenWidth;

    public float posOnScreenHeight;

    public bool selected;

    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        _effectDescription.text = _description;
    }
    
    void Update()
    { 
        //Debug.Log("base update question mark");
        
        if (selected)
        {
            myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * posOnScreenWidth, Screen.height * posOnScreenHeight, 0);
        }
    }
    
    public virtual void OnHighlightEnter()
    {
        myTips.SetActive(true);
        
        selected = true;
    }
    
    public virtual void OnHighlightExit()
    {
        if (myTips.activeSelf)
        {
            selected = false;
        
            myTips.SetActive(false);
        }
    }
}
