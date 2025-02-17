using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingB : MonoBehaviour
{
    public GameObject gears;
    
    public float speed;

    public Transform t;

    void Start()
    {
        gears = GameObject.FindWithTag("GameController");
    }
    
    void Update()
    {
        //savoir si qlq chose dans le monde est en dehors de l'écran
        var point = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(gears.GetComponent<Gears>().cam.pixelWidth, gears.GetComponent<Gears>().cam.pixelHeight));
        var point2 = gears.GetComponent<Gears>().cam.ScreenToWorldPoint(new Vector3(0, 0));

        if (transform.position.x >= point.x || transform.position.x <= point2.x || transform.position.y >= point.y || transform.position.y <= point2.y)
        {
            Destroy(gameObject);
        }
    }
}
