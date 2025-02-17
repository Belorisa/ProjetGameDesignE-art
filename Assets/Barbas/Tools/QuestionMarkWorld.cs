using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkWorld : MonoBehaviour
{
    public GameObject gears;
    
    public LayerMask playerLayer;
    public bool playerInRange;
    public bool textShowed;

    public GameObject showTextToThePlayer;
    public GameObject textInstantiated;
    public Transform showTextHere;
    
    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
    }
    
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, 3f, playerLayer);
        
        if (playerInRange)
        {
            ShowText();
            
            Vector2 textPos = Camera.main.WorldToScreenPoint(showTextHere.position);
            textInstantiated.transform.position = textPos;
        }
        else
        {
            Destroy(textInstantiated);
            textShowed = false;
        }
    }
    
    public void ShowText()
    {
        GameObject textInstantiated2 = Instantiate(showTextToThePlayer);
        textInstantiated = textInstantiated2;
        textInstantiated.transform.SetParent(gears.GetComponent<Gears>().uIManager.transform);
        textShowed = true;
    }
}
