using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionIndicator : MonoBehaviour
{
    public GameObject gears;
    
    public TextMeshProUGUI ammunitionText;

    public Sprite[] sprites;

    public Image myImage;
        
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }
    
    void Update()
    {
        
    }

    public void UpdateAmmunition()
    {
        ammunitionText.text = Gears.gears.playerI.GetComponent<Player>().ammunition + "/" +  Gears.gears.playerI.GetComponent<Player>().maxAmmunition;

        float f = gears.GetComponent<Gears>().playerI.GetComponent<Player>().ammunition /
                  gears.GetComponent<Gears>().playerI.GetComponent<Player>().maxAmmunition;
        
        //Debug.Log(f);
        
        if (f > 0.75f)
        {
            myImage.sprite = sprites[0];
        }
        else if (f > 0.5f)
        {
            myImage.sprite = sprites[1];
        }
        else if (f > 0.25f)
        {
            myImage.sprite = sprites[2];
        }
        else if (f > 0)
        {
            myImage.sprite = sprites[3];
        }
        else
        {
            myImage.sprite = sprites[4];
        }
    }
}
