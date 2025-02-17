using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    
    public bool mouseOnUi;
    
    public GameObject ammunitionPack;

    public float ammunitionPackThreshHold;

    public float decreaseSpeedAmmunitionThreshHold;

    public float soundThreshHold;

    public float decreaseSpeedSoundThreshHold;

    public int bonusChanceToDropAmmunitionPack;

    void Awake()
    {
        current = this;
        Gears.gears.eventManager = this;
        //besoin d'un event manager dans toutes les scenes
    }
    
    void Start()
    {
        
    }

    public event Action<int> OnSellTower;

    public void SellTowerTriggerEnter(int id)
    {
        if (OnSellTower != null)
        {
            OnSellTower(id);
        }
    }

    void Update()
    {
        ammunitionPackThreshHold -= Time.deltaTime * decreaseSpeedAmmunitionThreshHold;

        ammunitionPackThreshHold = Mathf.Clamp(ammunitionPackThreshHold, 0, 100);
        
        soundThreshHold -= Time.deltaTime * decreaseSpeedSoundThreshHold;
        
        soundThreshHold = Mathf.Clamp(soundThreshHold, 0, 100);
    }

    public void DropAmmunition(GameObject go, int ammunition)
    {
        if (ammunitionPackThreshHold <= 3)
        {
            GameObject iAmm = Instantiate(ammunitionPack, new Vector3(go.transform.position.x, go.transform.position.y, ammunitionPack.transform.position.z), 
                ammunitionPack.transform.rotation);
        
            iAmm.GetComponent<AmmunitionPackScript>().ammunition = ammunition;

            ammunitionPackThreshHold++;
        }
    }

    public void PlaySound(Sound s, int worth)
    {
        switch (worth)
        {
            default:
                Debug.Log("default play sound");
                break;
            case 0 :
                if (soundThreshHold <= 3)
                {
                    s.source.Play();
                    soundThreshHold++;
                }
                break;
            case 1 :
                if (soundThreshHold <= 5)
                {
                    s.source.Play();
                    soundThreshHold++;
                }
                break;
            case 2 :
                if (soundThreshHold <= 7)
                {
                    s.source.Play();
                    soundThreshHold++;
                }
                break;
        }
    }

    public void MouseOnUiEnter()
    {
        mouseOnUi = true;
    }
    
    public void MouseOnUiExit()
    {
        mouseOnUi = false;
    }
}
