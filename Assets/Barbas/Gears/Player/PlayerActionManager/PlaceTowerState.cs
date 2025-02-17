using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaceTowerState : StateTower
{
    public PlayerActionManager playerActionManager;

    public GameObject towerToPlace;

    public GameObject ghost;
    public GameObject ghostRangeI;

    public PlaceTowerState(PlayerActionManager playerActionManager, GameObject tower) : base(playerActionManager)
    {
        this.playerActionManager = playerActionManager;
        towerToPlace = tower;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Placing tower");

        playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>().ShowRange();
        
        
        ghost = playerActionManager.FuncInstantiate(playerActionManager.emptySprite, new Vector2(
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y), playerActionManager.emptySprite.transform);
        
        ghost.transform.localScale = towerToPlace.GetComponent<TowerV2>().towerR.transform.localScale;

        ghost.GetComponent<SpriteRenderer>().sprite = towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().sprite;

        ghost.GetComponent<Renderer>().material.color = new Color(towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.r,
            towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.g, towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.b,0.5f);

        ghostRangeI = playerActionManager.FuncInstantiate(playerActionManager.ghostRange, new Vector2(
            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y), playerActionManager.ghostRange.transform);

        ghostRangeI.transform.localScale = new Vector3(towerToPlace.GetComponent<TowerV2>().myStats.range * 2f,  towerToPlace.GetComponent<TowerV2>().myStats.range * 2f,  
            playerActionManager.ghostRange.transform.localScale.z);
    }
    public override void Tick()
    {
        Vector3 mousePos = Vector3.Lerp(new Vector2(
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y),
            ghost.transform.position, 0.15f); //juice
        
        ghost.transform.position = mousePos;
        ghostRangeI.transform.position = mousePos; //affichage

        if (!CanPlaceTower())
        {
            ghost.GetComponent<Renderer>().material.color = new Color(1, 0, 0,0.5f);
        }
        else if (ghost.GetComponent<Renderer>().material.color != new Color(towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.r,
            towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.g, towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.b,0.5f))
        {
            ghost.GetComponent<Renderer>().material.color = new Color(towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.r,
                towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.g, towerToPlace.GetComponent<TowerV2>().towerR.GetComponent<SpriteRenderer>().color.b,0.5f);
        }

        if (Input.GetButtonDown("Fire1") )
        {
            if (CanPlaceTower())
            {
               PlaceTower();
            }
            else
            {
                Debug.Log("Can't place Tower");
            }
        }
    }
    
    public override void OnStateExit()
    {
        playerActionManager.notEnoughGolds.SetActive(false);
        playerActionManager.DestroyObject(ghost);
        playerActionManager.DestroyObject(ghostRangeI);
        playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>().ShowRange();
    }
    
    public void PlaceTower()
    {
        //playerActionManager.mySounds.PlaySound("PlaceTowerSound");
        
        playerActionManager.golds -= towerToPlace.GetComponent<TowerV2>().myStats.goldCost;
        playerActionManager.FuncInstantiate(towerToPlace, new Vector3(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y, towerToPlace.transform.position.z) , towerToPlace.transform);
        
        //Debug.Log("Place tower");
        playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>().SetState(new PlayerBuildingState(
            playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>(), towerToPlace.GetComponent<TowerV2>().myStats.constructionTime));
        
        playerActionManager.SetState(null);
    }

    public bool CanPlaceTower()
    {
        /*if (playerActionManager.golds < towerToPlace.GetComponent<TowerV2>().myStats.goldCost)
        {
            playerActionManager.notEnoughGolds.SetActive(true);
            playerActionManager.notEnoughGolds.transform.position = Input.mousePosition + new Vector3(30,30,0);
            return false;
        }*/

        Collider2D[] towersClose = Physics2D.OverlapCircleAll(
            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition), playerActionManager.rangeBeetweenTowers, 
            playerActionManager.gears.GetComponent<Gears>().TowerLayer);
        
        if (towersClose.Length != 0)
        {
            return false;
        }

        if (Vector2.Distance(playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>().feets.transform.position,
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition)) >
            playerActionManager.gears.GetComponent<Gears>().playerI.GetComponent<Player>().actionRange)
        {
            return false;
        }

        if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition))) != null ||
            playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.WorldToCell(
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition))) != null)
        {
            return false;
        }

        /*Debug.Log(playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
            playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition))));*/
        
        return true;
    }
}
