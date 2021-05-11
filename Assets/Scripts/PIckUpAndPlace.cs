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

        Debug.Log(canBePlaced);
        
        if(canBePlaced && haspos)
        {
            transform.position = snapPos;
            snapImage.transform.position = transform.position;
        }
        else
        {
            transform.position = startPos;
            snapImage.transform.position = transform.position;
        }
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
        int x = currentCenterTile.GetComponent<TileInfo>().tileNum -1;
        int y = currentCenterTile.GetComponent<TileInfo>().lineNum -1;
        foreach (GridLine line in playerHand.grid)
        {
            if (y >= 0 && y < currentGrid.grid.Count)
            {
                foreach (Tile tile in line.line)
                {
                    if (x >= 0 && x < currentGrid.grid[y].line.Count)
                    {

                        switch (tile.OnTile)
                        {
                            case OnTile.Empty:
                                break;
                            case OnTile.House:
                                if (currentGrid.grid[y].line[x].tileType == TileType.Water || currentGrid.grid[y].line[x].OnTile == OnTile.House)
                                {
                                    //Debug.Log("nope");
                                    canBePlaced = false;
                                }
                                else if (currentGrid.grid[y].line[x].tileType == TileType.Ground && currentGrid.grid[y].line[x].OnTile == OnTile.Empty)
                                {
                                    //Debug.Log("is cool :)");
                                }
                                break;
                            case OnTile.X:
                                if (currentGrid.grid[y].line[x].OnTile == OnTile.House)
                                {
                                    //Debug.Log("downgrade");
                                }
                                break;

                        }
                    }
                    x++;

                }
            }
            x = currentCenterTile.GetComponent<TileInfo>().tileNum - 1;
            y++;
        }
    }
}
