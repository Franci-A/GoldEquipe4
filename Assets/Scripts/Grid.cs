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
        
        if(grid.Count ==0)
            InstanciateGrid();
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
            obj.color = Color.white;
        }
    }

    public void InstanciateGrid()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < (gridHeight+2) * (gridWidth+2); i++)
        {
            Tile obj = Instantiate(tilePrefab, transform).GetComponent<Tile>();
            grid.Add(obj);
            obj.transform.localPosition = new Vector3(x * cellSize, y * -1 * cellSize, 0) - drawOffset;
            obj.lineNum = y;
            obj.tileNum = x;
            if (y == 0 || y == gridHeight + 1 || x == 0 || x == gridWidth + 1)
                obj.tileType = TileType.Empty;
            if(randomGen)
                obj.tileType = (TileType)Random.Range(0, 2);
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
