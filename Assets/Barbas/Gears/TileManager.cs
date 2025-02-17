using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    private GameObject gears;

    public Tilemap tilemapTrail;

    public Tilemap tileMapBackGround;

    public Tilemap tileMapBridges;

    void Start()
    {
        gears = GameObject.FindGameObjectWithTag("GameController");
        
        gears.GetComponent<Gears>().tileManager = this;
    }
    
    void Update()
    {
       
    }
}
