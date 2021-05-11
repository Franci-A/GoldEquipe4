using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public List<GridLine> grid;
    [SerializeField] private GameObject tilePrefab;
    public Vector3 drawOffset;



    private void Start()
    {
        
        int x = 0;
        int y = 0;
        foreach (GridLine line in grid)
        {
            foreach (Tile tile in line.line)
            {
                GameObject obj = Instantiate(tilePrefab, transform);
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
                x++;
            }
            x = 0;
            y++;
        }
    }
}
