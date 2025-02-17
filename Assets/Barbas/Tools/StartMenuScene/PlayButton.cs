using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(Gears.gears.LoadGame);
    }
    
    void Update()
    {
        
    }
}
