using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlButtonScript : MonoBehaviour
{
    public LevelLoad myLevel;

    private Image _myImage;

    private TextMeshProUGUI myText;

    private Button _button;
    
    void Start()
    {
        _myImage = GetComponent<Image>();

        myText = GetComponentInChildren<TextMeshProUGUI>();

        _button = GetComponent<Button>();
        
        if (myLevel.imageOfTheLvl != null)
        {
            _myImage.sprite = myLevel.imageOfTheLvl;
        }

        myText.text = myLevel.text;
        
        _button.onClick.AddListener(WhenClicked);
    }
    
    void Update()
    {
        
    }

    public void WhenClicked()
    {
        SceneManager.LoadScene(myLevel.myScene);
    }
}

[System.Serializable]
public class LevelLoad
{
    public string myScene;

    public Sprite imageOfTheLvl;

    public String text;
}
