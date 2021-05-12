using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUpAndPlace : MonoBehaviour
{
    private bool isInHand;
    private Vector3 startPos;
    [SerializeField] private GameObject snapImage;
    [SerializeField] private Vector3 snapPos;
    private bool haspos;
    private GameObject currentCenterTile;
    public Grid playerHand;
    public Grid currentGrid;
    private bool canBePlaced;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isInHand)
        {
            //follow cursor
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            if (haspos)
            {
                snapImage.transform.position = snapPos;
            }
            else
            {
                snapImage.transform.position = transform.position;
            }
        }
    }

    private void OnMouseDown()
    {
        isInHand = true;
    }

    private void OnMouseUp()
    {
        isInHand = false;
        
        if(canBePlaced && haspos)
        {
            PlaceTiles();
            GetComponent<PlayerPieceManager>().NextTurn();
        }

        transform.position = startPos;
        snapImage.transform.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            haspos = true;
            snapPos = collision.transform.position;
            currentCenterTile = collision.gameObject;
            CheckPlacement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentCenterTile)
        {
            haspos = false;
        }
    }

    private void CheckPlacement()
    {
        canBePlaced = true;
        int x = currentCenterTile.GetComponent<Tile>().tileNum -1;
        int y = currentCenterTile.GetComponent<Tile>().lineNum -1;
        foreach (Tile tile in playerHand.grid)
        {
            if (y < 0 || y >= currentGrid.gridHeight)
            {
                x++;
                if (x > currentCenterTile.GetComponent<Tile>().tileNum + 1)
                {
                    x = currentCenterTile.GetComponent<Tile>().tileNum - 1;
                    y++;
                }
            }
            else
            {
                if (x >= 0 && x < currentGrid.gridWidth)
                {

                    switch (tile.tileType)
                    {
                        case TileType.Empty:
                            
                            break;
                        case TileType.House:
                            if (currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.Water || currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.House || currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.Empty)
                            {
                                canBePlaced = false;
                            }
                            else if (currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.Ground)
                            {
                                //Debug.Log("can be placed");
                            }
                            break;
                        case TileType.X:
                            if (currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.House)
                            {
                                //Debug.Log("downgrade");
                            }
                            break;

                    }
                }
                x++;

                if (x > currentCenterTile.GetComponent<Tile>().tileNum + 1)
                {
                    x = currentCenterTile.GetComponent<Tile>().tileNum - 1;
                    y++;
                }
            }
        }
    }

    public void PlaceTiles()
    {
        int x = currentCenterTile.GetComponent<Tile>().tileNum - 1;
        int y = currentCenterTile.GetComponent<Tile>().lineNum - 1;
        foreach (Tile tile in playerHand.grid)
        {
            if (y < 0 || y >= currentGrid.gridHeight)
            {
                x++;
                if (x > currentCenterTile.GetComponent<Tile>().tileNum + 1)
                {
                    x = currentCenterTile.GetComponent<Tile>().tileNum - 1;
                    y++;
                }
            }
            else if (x >= 0 && x < currentGrid.gridWidth)
            {
                switch (tile.tileType)
                {
                    case TileType.House:
                        currentGrid.grid[y * currentGrid.gridWidth + x].tileType = TileType.House;
                        currentGrid.grid[y * currentGrid.gridWidth + x].houseUpgrade++;
                        currentGrid.grid[y * currentGrid.gridWidth + x].houseColor = tile.houseColor;
                        currentGrid.grid[y * currentGrid.gridWidth + x].GetComponent<Merge>().merging();
                        currentGrid.UpdateTile(y, x);
                        break;
                    case TileType.X:
                        if (currentGrid.grid[y * currentGrid.gridWidth + x].tileType == TileType.House)
                        {
                            currentGrid.grid[y * currentGrid.gridWidth + x].houseUpgrade = 0;
                            currentGrid.grid[y * currentGrid.gridWidth + x].tileType = TileType.Ground;
                            currentGrid.UpdateTile(y, x);
                        }
                        break;
                }
            }
            x++;

            if (x > currentCenterTile.GetComponent<Tile>().tileNum + 1)
            {
                x = currentCenterTile.GetComponent<Tile>().tileNum - 1;
                y++;
            }
        }
    }
}
