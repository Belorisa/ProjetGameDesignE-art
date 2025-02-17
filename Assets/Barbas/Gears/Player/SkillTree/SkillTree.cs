using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public GameObject gears;
    
    public TextMeshProUGUI passivPointText;

    public int passivPoint;
    
    //stats boostable
    public float towerDamageBoost = 1;

    public float playerDamageMultiplier = 1;

    //public GameObject[] points;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }
    
    void Update()
    {
        passivPointText.text = "Passiv Points : " + passivPoint;
    }
}
