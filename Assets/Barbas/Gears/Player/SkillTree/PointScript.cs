using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour
{
    private GameObject gears;
    
    public SkillTree skillTreeLinkedTo;

    public bool reached;

    public Button myButton;

    public int passivPointCost;

    public PointScript[] pointsLinkedWith; //que les points qui si ils sont prit ont peut prendre celui la

    public GameObject empty;
    
    public List<LineUITest> myLines = new List<LineUITest>();
    
    public GameObject myTips;
    public TextMeshProUGUI passivPointCostText;
    public TextMeshProUGUI effectDescription;

    public float tipsPosOnScreenWidth;
    public float tipsPosOnScreenHeight;

    private float _distanceMouse;

    public string description;

    private bool _highLighted;

    public void Awake()
    {
        //StartForEvryPoint();
    }
    
    public void Update()
    {
        _distanceMouse = Vector2.Distance(transform.position, new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        for (int i = 0; i < pointsLinkedWith.Length; i++)
        {
            if (pointsLinkedWith[i].reached)
            {
                myLines[i].color = Color.yellow;
            }
        }

        if (_highLighted)
        {
            myTips.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width * tipsPosOnScreenWidth, Screen.height * tipsPosOnScreenHeight, 
                myTips.GetComponent<RectTransform>().transform.position.z);
        }
    }
    
    public void StartForEvryPoint()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        effectDescription.text = description;

        myButton?.onClick.AddListener(AskToGet);

        if (pointsLinkedWith.Length != 0)
        {
            foreach (var point in pointsLinkedWith)
            {
                GameObject line = Instantiate(empty, gears.GetComponent<Gears>().uIManager.transform);
                line.AddComponent<RectTransform>();
                line.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
                line.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 35);
                
                LineUITest t = line.AddComponent<LineUITest>();
                myLines.Add(t);
                line.GetComponent<LineUITest>().Points = new Vector2[2];
                
                line.GetComponent<LineUITest>().Points[0] = GetComponent<RectTransform>().localPosition;
                line.GetComponent<LineUITest>().Points[1] = new Vector2(point.GetComponent<RectTransform>().localPosition.x, point.GetComponent<RectTransform>().localPosition.y + 30);
                line.transform.SetParent(transform);
            }
        }

        if (passivPointCostText != null)
        {
            passivPointCostText.text = "Cost : " + passivPointCost + " Passiv Point";
        }
    }

    public virtual void Effect()
    {
        Debug.Log("Base Effect");
    }

    public void AskToGet()
    {
        //Debug.Log("Ask to get");
        if (CanGetPoint())
        {
            //Debug.Log("Get Passiv point");
            Effect(); 
            skillTreeLinkedTo.passivPoint -= passivPointCost;
            reached = true;
            
            GetComponent<Image>().color = Color.yellow;

            List<int> indexes = new List<int>();

            for (int i = 0; i < pointsLinkedWith.Length; i++)
            {
                if (pointsLinkedWith[i].reached)
                {
                    indexes.Add(i);
                }
            }
           
            foreach (var index in indexes)
            {
                myLines[index].color = Color.yellow;
            }
        }
    }
    
    public bool CanGetPoint()
    {
        if (reached)
        {
            Debug.Log("you already have this point");
            return false;
        }

        int i = 0;
        
        foreach (var point in pointsLinkedWith)
        {
            if (point.reached)
            {
                i++;
            }
        }

        if (i == 0)
        {
            return false;
        }
        
        if (passivPointCost > skillTreeLinkedTo.passivPoint)
        {
            return false;
        }

        return true;
    }
    
    public void OnHighlightEnter()
    {
        myTips.SetActive(true);

        _highLighted = true;
    }

    public void OnHighlightExit()
    {
        if (myTips.activeSelf)
        {
            _highLighted = false;
            
            myTips.SetActive(false);
        }
    }

    /*public void UpdateEvryOne()
    {
        distanceMouse = Vector2.Distance(transform.position, new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        
        if (distanceMouse <= 30)
        {
            myTips.SetActive(true);
        }
        else if (myTips.activeSelf)
        {
            myTips.SetActive(false);
        }
    }

    public void EnabledAll()
    {
        for (int i = 0; i < pointsLinkedWith.Length; i++)
        {
            if (pointsLinkedWith[i].reached)
            {
                myLines[i].color = Color.yellow;
            }
        }
    }*/
}
