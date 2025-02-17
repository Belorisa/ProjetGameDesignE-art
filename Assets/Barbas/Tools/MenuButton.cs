using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button myButton;
    
    void Start()
    {
        myButton = GetComponent<Button>();
        
        myButton.onClick.AddListener(Gears.gears.LoadMenu);
    }
    
    void Update()
    {
        
    }
}
