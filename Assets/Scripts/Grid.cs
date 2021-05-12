using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Grid : MonoBehaviour
{
    public List<Tile> grid;
    public int gridHeight;
    public int gridWidth;
    public bool randomGen;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float cellSize;
    public Vector3 drawOffset;
    private SpriteLibrary sprites;


    private void Start()
    {
        sprites = GetComponent<SpriteLibrary>();
        gridHeight += 2;
        gridWidth += 2;
        if(grid.Count ==0)
            InstanciateGrid();
        else
        {
            int x = 0;
            int y = 0;
            foreach (Tile tile in grid)
            {
                tile.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
                tile.lineNum = y;
                tile.tileNum = x;
                UpdateTile(tile.lineNum, tile.tileNum);
                x++;
                if(x == gridWidth)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }

    public void UpdateTile(int line, int tile)
    {
        SpriteRenderer obj = grid[gridWidth * line + tile].transform.GetChild(0).GetComponent<SpriteRenderer>();
        obj.sprite = sprites.GetSprite("House", "level" + grid[gridWidth * line + tile].houseUpgrade.ToString());
        if (grid[gridWidth * line + tile].houseUpgrade == 0)
        {
            obj.color = Color.clear;
        }
        else
        {
            switch (grid[gridWidth * line + tile].houseColor)
            {
                case HouseColor.Blue:
                    obj.color = Color.blue;
                    break;
                case HouseColor.Red:
                    obj.color = Color.red;
                    break;
                case HouseColor.Green:
                    obj.color = Color.green;
                    break;
                case HouseColor.Yollow:
                    obj.color = Color.yellow;
                    break;
            }
        }
    }

    public void InstanciateGrid()
    {
        int x = 0;
        int y = 0;
        int maxWaterTiles = 5;
        for (int i = 0; i < (gridHeight) * (gridWidth); i++)
        {
            Tile obj = Instantiate(tilePrefab, transform).GetComponent<Tile>();
            grid.Add(obj);
            obj.transform.localPosition = new Vector3(x * cellSize, y * -1 * cellSize, 0) - drawOffset;
            obj.lineNum = y;
            obj.tileNum = x;
            if (y == 0 || y == gridHeight - 1 || x == 0 || x == gridWidth - 1)
                obj.tileType = TileType.Empty;
            else if (randomGen)
            {
                float k = Random.Range(0, 1f);
                if (k > .9f && maxWaterTiles > 0)
                {
                    obj.tileType = TileType.Water;
                    maxWaterTiles--;
                }
                else
                {
                    obj.tileType = TileType.Ground;
                }
            }
            switch (obj.tileType)
            {
                case TileType.Ground:
                    obj.GetComponent<SpriteRenderer>().color = Color.green;
                    break;

                case TileType.Water:
                    obj.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;

                case TileType.Empty:
                    obj.GetComponent<SpriteRenderer>().color = Color.clear;
                    break;
                case TileType.House:
                    obj.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = sprites.GetSprite("House", "level" + obj.houseUpgrade.ToString());
                    break;
                case TileType.X:
                    obj.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = sprites.GetSprite("House", "X");
                    break;
            }
            x++;
            if (x >= gridWidth)
            {
                y++;
                x = 0;
            }
        }
    }
    
}
