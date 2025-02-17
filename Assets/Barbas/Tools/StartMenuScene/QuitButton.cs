using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public GameObject gears;
    
    public Button button;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");

        button = GetComponent<Button>();
        
        button.onClick.AddListener(gears.GetComponent<Gears>().Quit);
    }
    
    void Update()
    {
        
    }
}
