using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUpAndPlace : MonoBehaviour
{
    private bool isInHand;
    private Score score;
    private Vector3 startPos;
    [SerializeField] private GameObject snapImage;
    [SerializeField] private GameObject outline;
    [SerializeField] private Vector3 snapPos;
    private bool haspos;
    private GameObject currentCenterTile;
    public Grid playerHand;
    public Grid currentGrid;
    private bool canBePlaced;
    private Vector2 offset;


    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        startPos = transform.position;
    }

    void Update()
    {
        if (isInHand)
        {
            //follow cursor
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) + (Vector3)offset;
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
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        foreach (Tile tile in playerHand.grid)
        {
            if(tile.tileType == TileType.House)
                tile.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        }
    }

    private void OnMouseUp()
    {
        isInHand = false;
        
        if(canBePlaced && haspos)
        {
            //Handheld.Vibrate();
            PlaceTiles();
            GetComponent<PlayerPieceManager>().NextTurn();
            if(!CheckPosibilities() || score.currentScore< 0)
            {
                Debug.Log("Defeat");
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver();
            }
        }

        transform.position = startPos;
        snapImage.transform.position = transform.position;
        foreach (Tile tile in playerHand.grid)
        {
            if (tile.tileType == TileType.House)
                tile.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
            if (canBePlaced)
            {
                outline.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                outline.GetComponent<SpriteRenderer>().color = Color.red;
            }
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
                if (tile.tileType == TileType.House)
                    canBePlaced = false;
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
                else if (tile.tileType == TileType.House)
                {
                    canBePlaced = false;
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
        List<int> tileWithHouse = new List<int>();
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
                        case TileType.House:
                            currentGrid.grid[y * currentGrid.gridWidth + x].tileType = TileType.House;
                            currentGrid.grid[y * currentGrid.gridWidth + x].houseUpgrade++;
                            currentGrid.grid[y * currentGrid.gridWidth + x].houseColor = tile.houseColor;
                            tileWithHouse.Add(y * currentGrid.gridWidth + x);
                            
                            break;
                        case TileType.X:
                            if (currentGrid.grid[y * currentGrid.gridWidth + x].tileType == TileType.House)
                            {
                                currentGrid.grid[y * currentGrid.gridWidth + x].houseUpgrade = 0;
                                currentGrid.grid[y * currentGrid.gridWidth + x].tileType = TileType.Ground;
                                currentGrid.grid[y * currentGrid.gridWidth + x].GetComponent<Animator>().SetTrigger("Downgrade");
                                score.AddScore(-10);
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
        foreach (int position in tileWithHouse)
        {
            currentGrid.grid[position].GetComponent<Merge>().merging();
            currentGrid.UpdateTile(position/currentGrid.gridWidth, position%currentGrid.gridWidth);
        }
    }

    public bool CheckPosibilities()
    {
        int index =0;
        for (int i = 0; i < 9; i++)
        {
            if (playerHand.grid[i].tileType == TileType.House)
            {
                index = i;
                break;
            }
        }
        
        int x = 1;
        int y = 1;
        for (int i = 0; i < (currentGrid.gridHeight -2) * (currentGrid.gridWidth -2); i++)
        {
            if(currentGrid.grid[currentGrid.gridWidth * y + x].tileType == TileType.Ground)
            {
                bool canBePlaced = true ;
                for (int j = index + 1; j < 9; j++)
                {
                    if (playerHand.grid[j].tileType == TileType.House)
                    {
                        int temp = j - index;
                        int currentPos = currentGrid.gridWidth * (y + temp / 3) + (x + temp % 3);
                        if (currentPos < 0 || currentPos >= currentGrid.grid.Count || currentGrid.grid[currentPos].tileType != TileType.Ground)
                        {
                            canBePlaced = false;
                            break;
                        }
                    }

                }
                if (canBePlaced)
                {
                    return canBePlaced;
                }

            }

            x++;
            if(x >= currentGrid.gridWidth)
            {
                x = 1;
                y++;
            }
        }

        return false;
    }
}
