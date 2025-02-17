using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleButton : MonoBehaviour
{
    public GameObject gears;
    
    public Button myButton;

    private bool pressed;
    
    public Sprite normalTime;
    public Sprite fastTime;

    public GameObject action;
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        myButton.onClick.AddListener(IncreaseTimeScale);
        myButton.onClick.AddListener(Switch);
    }
    
    void Update()
    {
        //Debug.Log(Time.timeScale);
    }
    
    public void IncreaseTimeScale()
    {
        if (!pressed)
        {
            Time.timeScale = 3.0f;
            
            gameObject.GetComponent<Image>().sprite = fastTime;
        }
        else
        {
            Time.timeScale = 1.0f;
            
            gameObject.GetComponent<Image>().sprite = normalTime;

        }

        pressed = !pressed;
    }

    public void Switch()
    {
        action.GetComponent<PlayerActionManager>().x2ButtonPressed = true;
    }
}
