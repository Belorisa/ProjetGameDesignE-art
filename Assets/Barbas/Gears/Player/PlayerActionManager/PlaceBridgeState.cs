using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class PlaceBridgeState :StateTower
{
    private PlayerActionManager playerActionManager;

    private GameObject _ghost;

    private GameObject _ghost2;

    private Vector2 mousePos;

    private int _i3;

    public PlaceBridgeState(PlayerActionManager playerActionManager) : base(playerActionManager)
    {
        this.playerActionManager = playerActionManager;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Place Bridge State");

        _ghost = playerActionManager.FuncInstantiate(playerActionManager.emptySprite,new Vector2(
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y), playerActionManager.emptySprite.transform);
        
        _ghost.GetComponent<SpriteRenderer>().sprite = playerActionManager.bridgeTile.sprite;
    }
    public override void Tick()
    {
         mousePos = Vector2.Lerp(new Vector2(
                 playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y),
            _ghost.transform.position, 0.15f); //juice

        _ghost.transform.position = mousePos;
        
        if (!CanPlaceBridge())
        {
            if (_ghost2 != null)
            {
                _ghost2.GetComponent<Renderer>().material.color = new Color(1, 0, 0,0.5f);
            }
            
            _ghost.GetComponent<Renderer>().material.color = new Color(1, 0, 0,0.5f);
        }
        else
        {
            if (_ghost2 != null && _ghost2.GetComponent<Renderer>().material.color != new Color(playerActionManager.bridgeTile.color.r, playerActionManager.bridgeTile.color.g,
                playerActionManager.bridgeTile.color.b, 0.5f))
            {
                _ghost2.GetComponent<Renderer>().material.color =  new Color(playerActionManager.bridgeTile.color.r, playerActionManager.bridgeTile.color.g,
                    playerActionManager.bridgeTile.color.b, 0.5f);
            }

            if (_ghost.GetComponent<Renderer>().material.color != new Color(playerActionManager.bridgeTile.color.r, playerActionManager.bridgeTile.color.g,
                    playerActionManager.bridgeTile.color.b, 0.5f))
            {
                _ghost.GetComponent<Renderer>().material.color =  new Color(playerActionManager.bridgeTile.color.r, playerActionManager.bridgeTile.color.g,
                    playerActionManager.bridgeTile.color.b, 0.5f);
            }
        }

        if (_i3 >= 0)
        {
            playerActionManager.bridgeTile = playerActionManager.bridgeTileVertical;
        }
        else
        {
            playerActionManager.bridgeTile = playerActionManager.bridgeTileHorizontal;
        }

        if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(playerActionManager.gears
                .GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y))) != null)
        {
            if (_i3 >= 0)
            {
                playerActionManager.bridgeTile = playerActionManager.bridgeTileVertical;
                
                ShowBridge2(new Vector2(0,1));
            }
            else
            {
                playerActionManager.bridgeTile = playerActionManager.bridgeTileHorizontal;
                
                ShowBridge2(new Vector2(1,0));
            }
        }
        else if (_ghost2 != null)
        {
            playerActionManager.DestroyObject(_ghost2);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (CanPlaceBridge())
            {
                if (_i3 >= 0) //horizontal
                {
                    PlaceBridge(new Vector2(0,1));
                }
                else
                {
                    PlaceBridge(new Vector2(1,0));
                }
            }
            else
            {
                Debug.Log("Can't place Bridge");
            }
        }
    }

    public void ShowBridge2(Vector2 v)
    {
        if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(playerActionManager.gears
                    .GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + v)) != null)
        { 
            if (_ghost2 == null)
            {
                _ghost2 = playerActionManager.FuncInstantiate(playerActionManager.emptySprite, new Vector2(
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y), playerActionManager.emptySprite.transform);
            } 
            
            _ghost2.GetComponent<SpriteRenderer>().sprite = playerActionManager.bridgeTile.sprite;

            _ghost2.transform.position = mousePos + v;
        }
        else
        {
            if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(playerActionManager.gears
                    .GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) - v)) != null)
            {
                if (_ghost2 == null)
                {
                    _ghost2 = playerActionManager.FuncInstantiate(playerActionManager.emptySprite, new Vector2(
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y), playerActionManager.emptySprite.transform);
                }
                
                _ghost2.GetComponent<SpriteRenderer>().sprite = playerActionManager.bridgeTile.sprite;

                _ghost2.transform.position = mousePos - v;
            }
        }
        
        _ghost.GetComponent<SpriteRenderer>().sprite = playerActionManager.bridgeTile.sprite;
    }

    private void PlaceBridge(Vector2 v)
    {
        //metter le pont sur la souris
        
        playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.SetTile(
            playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)), null);

        playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.SetTile(
            playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.WorldToCell(
                new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)), playerActionManager.bridgeTile);
        
        //mettre le pont à coté de la souris
        
        if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(playerActionManager.gears
                .GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + v)) != null)
        {
            SetTileBridge(v, false);
        }

        if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(playerActionManager.gears
                .GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) - v)) != null)
        {
           SetTileBridge(v, true);
        }

        playerActionManager.bridgesLeft--;
        
        //TODO : créer un évenement pour réduire les dépendances
        
        playerActionManager.gears.GetComponent<Gears>().uIManager.transform.GetChild(2).transform.GetChild(7).GetComponent<TowerMenuUIBridges>().
            UpdateBridgesNumberText(playerActionManager.bridgesLeft);
        
        AudioSource.PlayClipAtPoint(playerActionManager.posePont, new Vector3(playerActionManager.transform.position.x,playerActionManager.transform.position.y,-8), 1.0f);

        playerActionManager.SetState(null);
    }

    public void SetTileBridge(Vector2 v, bool negativeVector)
    {
        if (!negativeVector)
        {
            playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.SetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + v), playerActionManager.bridgeTile);
            
            playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.SetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + v), null);
        }
        else
        {
            playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.SetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tileMapBridges.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) - v), playerActionManager.bridgeTile);
            
            playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.SetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) - v), null);
        }
    }

    public bool CanPlaceBridge()
    {
        if (OneLine())
        {
            return true;
        }

        bool OneLine()
        {
            if (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y))) == null)
            {
                //Debug.Log("Tile null");
                return false;
            }

            int i1 = 0;

            var t1 = playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)));
            
            var t2 = playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                    new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                        playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)));

            while (t1 != null && t2 != null)
            {
                t1 = (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + new Vector2(i1,0))));
                
                t2 = (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + new Vector2(-i1,0))));


                i1++;
            }

            int i2 = 0;
                
            var t3 = playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(new Vector2(
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)));
            
            var t4 = playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(new Vector2(
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x,
                    playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y)));
                
            while (t3 != null && t4 != null)
            {
                t3 = (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + new Vector2(0,i2))));
                
                t4 = (playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.GetTile(
                    playerActionManager.gears.GetComponent<Gears>().tileManager.tilemapTrail.WorldToCell(
                        new Vector2(playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).x, 
                            playerActionManager.gears.GetComponent<Gears>().cam.ScreenToWorldPoint(Input.mousePosition).y) + new Vector2(0,-i2))));

                i2++;
            }

            _i3 = i1 - i2;

            if (Mathf.Abs(_i3) >= 2)
            { 
                return true;
            }

            return false;
        }

        return false;
    }
    
    public override void OnStateExit()
    {
        playerActionManager.DestroyObject(_ghost);
        
        playerActionManager.DestroyObject(_ghost2);
    }
}
