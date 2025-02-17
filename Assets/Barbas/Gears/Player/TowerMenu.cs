using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    private float progression;

    private float lateProgression;

    public float speed;
    public float height;
    public float width;
    
    private float height2;
    private float width2;

    public float rangeBetweenButtons;

    public GameObject[] towerButtons;

    //public List<Vector3> buttonsBaseScale = new List<Vector3>();

    void Start()
    {
        width2 = Screen.width * width;
        height2 = Screen.width * height;


        if ((int) width2 == (int) height2) //si cercle
        {
            float perfectDistanceBetweenButtons = 2 * Mathf.PI / towerButtons.Length;

            rangeBetweenButtons = perfectDistanceBetweenButtons;
        }

        /*foreach (var button in towerButtons)
        {
            buttonsBaseScale.Add(button.transform.localScale);
        }*/
        
         float diff = 0;
        
        foreach (var button in towerButtons)
        {
            //int index = 0;
            
            float x = Mathf.Cos(lateProgression + diff + Mathf.PI * 0.5f) * width2;
            float y = Mathf.Sin(lateProgression + diff + Mathf.PI * 0.5f) * height2;
            
            button.GetComponent<RectTransform>().position =  new Vector2(GetComponent<RectTransform>().position.x + x, GetComponent<RectTransform>().position.y + y);
            
            diff += rangeBetweenButtons;
        }
    }
    
    void Update()
    {
        progression += Input.mouseScrollDelta.y * speed;

        lateProgression = Mathf.Lerp(lateProgression, progression, 0.1f);

        float diff = 0;
        
        foreach (var button in towerButtons)
        {
            //int index = 0;
            
            float x = Mathf.Cos(lateProgression + diff + Mathf.PI * 0.5f) * width2;
            float y = Mathf.Sin(lateProgression + diff + Mathf.PI * 0.5f) * height2;
            
            button.GetComponent<RectTransform>().position =  new Vector2(GetComponent<RectTransform>().position.x + x, GetComponent<RectTransform>().position.y + y);
            
            diff += rangeBetweenButtons;

            //float prog = (lateProgression + diff) / (2 * Mathf.PI);
            
            /*if (prog > 1) //changement d'alpha
            {
                float a = (-10 / 3) * (prog - 1) * (prog - 1) + (17 / 6) * (prog - 1) + 0.5f;
                
                float scaleMult = 1.2f * a;

                button.transform.localScale = buttonsBaseScale[index] * scaleMult;
                
                button.GetComponent<Image>().color = new Color(button.GetComponent<Image>().color.r,button.GetComponent<Image>().color.g,button.GetComponent<Image>().color.b, a);
                
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(button.GetComponentInChildren<TextMeshProUGUI>().color.r,
                    button.GetComponentInChildren<TextMeshProUGUI>().color.g,button.GetComponentInChildren<TextMeshProUGUI>().color.b, a);
                
                button.GetComponentInChildren<Text>().color = new Color(button.GetComponentInChildren<Text>().color.r,button.GetComponentInChildren<Text>().color.g,
                    button.GetComponentInChildren<Text>().color.b, a);
                
                if (prog - 1 > 0.5f)
                {
                    button.GetComponent<Button>().enabled = false;
                }
                else
                {
                    button.GetComponent<Button>().enabled = true;
                }
            }
            else if (prog < 0)
            {
                float b = (-10 / 3) * (prog + 1) * (prog + 1) + (17 / 6) * (prog + 1) + 0.5f;
                
                float scaleMult = 1.2f * b;

                button.transform.localScale = buttonsBaseScale[index] * scaleMult;
                
                button.GetComponent<Image>().color = new Color(button.GetComponent<Image>().color.r,button.GetComponent<Image>().color.g,button.GetComponent<Image>().color.b, b);
                
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(button.GetComponentInChildren<TextMeshProUGUI>().color.r,
                    button.GetComponentInChildren<TextMeshProUGUI>().color.g,button.GetComponentInChildren<TextMeshProUGUI>().color.b, b);
                
                button.GetComponentInChildren<Text>().color = new Color(button.GetComponentInChildren<Text>().color.r,button.GetComponentInChildren<Text>().color.g,
                    button.GetComponentInChildren<Text>().color.b, b);
                
                if (prog + 1 > 0.5f)
                {
                    button.GetComponent<Button>().enabled = false;
                }
                else
                {
                    button.GetComponent<Button>().enabled = true;
                }
            }
            else
            {
                float c = (-10 / 3) * (prog * prog) + (17 / 6) * (prog) + 0.5f;
                
                float scaleMult = 1.2f * c;

                button.transform.localScale = buttonsBaseScale[index] * scaleMult;
                
                button.GetComponent<Image>().color = new Color(button.GetComponent<Image>().color.r,button.GetComponent<Image>().color.g,button.GetComponent<Image>().color.b, c);
                
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(button.GetComponentInChildren<TextMeshProUGUI>().color.r,
                    button.GetComponentInChildren<TextMeshProUGUI>().color.g,button.GetComponentInChildren<TextMeshProUGUI>().color.b, c);
                
                button.GetComponentInChildren<Text>().color = new Color(button.GetComponentInChildren<Text>().color.r,button.GetComponentInChildren<Text>().color.g,
                    button.GetComponentInChildren<Text>().color.b, c);
                
                if (prog > 0.5f)
                {
                    button.GetComponent<Button>().enabled = false;
                }
                else
                {
                    button.GetComponent<Button>().enabled = true;
                }
            }*/
            //index++;
        }

        if (lateProgression > 2 * Mathf.PI || lateProgression < -2 * Mathf.PI)
        {
            progression = 0;
            lateProgression = 0;
        }
    }
}
