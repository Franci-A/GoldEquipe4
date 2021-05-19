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
    [SerializeField] private float waterTileSpawnChance;
    [SerializeField] private int maxWaterTiles;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float cellSize;
    public Vector3 drawOffset;
    private SpriteLibrary sprites;


    private void Start()
    {
        drawOffset = new Vector3(tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x * gridWidth / 2 + tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, drawOffset.y, 0);
        sprites = GetComponent<SpriteLibrary>();
        gridHeight += 2;
        gridWidth += 2;
        if (grid.Count == 0)
        {
            InstanciateGrid();
        }
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
                if (x == gridWidth)
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
        Tile objTile = obj.gameObject.GetComponentInParent<Tile>();
        obj.sprite = sprites.GetSprite("House", "level" + grid[gridWidth * line + tile].houseUpgrade.ToString());
        if (grid[gridWidth * line + tile].houseUpgrade == 0)
        {
            obj.sprite = sprites.GetSprite(objTile.houseColor.ToString(), "level" + objTile.houseUpgrade.ToString());
            obj.color = Color.clear;
        }
        else
        {
            obj.color = Color.white;
            obj.sprite = sprites.GetSprite(objTile.houseColor.ToString(), "level" + objTile.houseUpgrade.ToString());
        }
    }

    //Create grid 
    public void InstanciateGrid()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < (gridHeight) * (gridWidth); i++)
        {
            Tile obj = Instantiate(tilePrefab, transform).GetComponent<Tile>();
            grid.Add(obj);
            obj.gameObject.transform.position = new Vector3(x * cellSize, y * -1 * cellSize, 0) - drawOffset;
            obj.lineNum = y;
            obj.tileNum = x;
            obj.GetComponent<SpriteRenderer>().sortingOrder = y;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = y + 9;
            if (y == 0 || y == gridHeight - 1 || x == 0 || x == gridWidth - 1)
                obj.tileType = TileType.Empty;
            else if (randomGen)
            {
                float k = Random.Range(0, 1f);
                if (k < waterTileSpawnChance && maxWaterTiles > 0)
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
                    obj.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Ground");
                    break;

                case TileType.Water:
                    obj.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Ground");
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Water");
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    break;

                case TileType.Empty:
                    obj.GetComponent<SpriteRenderer>().color = Color.clear;
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
