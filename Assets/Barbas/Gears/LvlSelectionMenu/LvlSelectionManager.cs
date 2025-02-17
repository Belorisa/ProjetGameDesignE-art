using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvlSelectionManager : MonoBehaviour
{
    public GameObject canvasScene;
    
    public int levelPerLine;

    public float betweenLevelsPercentOfTheScreen;

    public float leftOfTheScreenPercentOfTheScreenBetweenLevelAnd;

    public float linesPercentOfTheScreenBetween;

    public GameObject[] lvlButtons;

    void Start()
    {
        int i = 0;

        int line = 0;

        Vector3 rectTransformPreviousButton = Vector3.zero;
        
        foreach (var button1 in lvlButtons)
        {
            bool placed = false;

            GameObject button = Instantiate(button1, canvasScene.transform, false);
            
            if (i == 0) //premier bouton
            {
                button.transform.position = new Vector3(Screen.width * leftOfTheScreenPercentOfTheScreenBetweenLevelAnd, 
                    Screen.height * 0.8f - Screen.height * linesPercentOfTheScreenBetween * line, 0);

                placed = true;
            }

            if (i % levelPerLine == 0) //passage de ligne
            {
                if (!placed)
                {
                    line++;
                    button.transform.position = new Vector3(Screen.width * leftOfTheScreenPercentOfTheScreenBetweenLevelAnd, 
                        Screen.height * 0.8f - Screen.height * linesPercentOfTheScreenBetween * line, button.transform.position.z);

                    placed = true;
                }
            }
            else if (!placed) //bouton place sur une ligne
            {
                button.transform.position = new Vector3(rectTransformPreviousButton.x + Screen.width * betweenLevelsPercentOfTheScreen, 
                    Screen.height * 0.8f - Screen.height * linesPercentOfTheScreenBetween * line, button.transform.position.z);

                placed = true;
            }

            rectTransformPreviousButton = button.transform.position;

            i++;
        }
    }
    
    void Update()
    {
        
    }
}
