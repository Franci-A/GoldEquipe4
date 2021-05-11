using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Grid : MonoBehaviour
{
    public List<GridLine> grid;
    [SerializeField] private GameObject tilePrefab;
    public Vector3 drawOffset;
    private SpriteLibrary sprites;
    private List<GameObject> tiles;


    private void Start()
    {
        tiles = new List<GameObject>(grid.Count * grid[0].line.Count);
        sprites = GetComponent<SpriteLibrary>();
        int x = 0;
        int y = 0;
        foreach (GridLine line in grid)
        {
            foreach (Tile tile in line.line)
            {
                GameObject obj = Instantiate(tilePrefab, transform);
                tiles.Add(obj);
                obj.transform.localPosition = new Vector3(x , y *-1 , 0) - drawOffset;
                obj.GetComponent<TileInfo>().lineNum = y;
                obj.GetComponent<TileInfo>().tileNum = x;
                
                switch (tile.tileType)
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
                }
                switch (tile.OnTile)
                {
                    case OnTile.Empty:
                        //obj.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
                        break;
                    case OnTile.House:
                        obj.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                        obj.GetComponentInChildren<SpriteRenderer>().sprite = sprites.GetSprite("House", "level" + tile.houseUpgrade.ToString());
                        break;
                    case OnTile.X:
                        obj.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                        obj.GetComponentInChildren<SpriteRenderer>().sprite = sprites.GetSprite("House", "X");
                        break;
                }
                x++;
            }
            x = 0;
            y++;
        }
    }

    public void UpdateTile(int line, int tile)
    {
        tiles[grid[0].line.Count * line + tile].GetComponentInChildren<SpriteRenderer>().sprite = sprites.GetSprite("House", "level" + grid[line].line[tile].houseUpgrade.ToString());
        if(grid[line].line[tile].houseUpgrade == 0)
        {
            tiles[grid[0].line.Count * line + tile].GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        }
        else
        {
            tiles[grid[0].line.Count * line + tile].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
