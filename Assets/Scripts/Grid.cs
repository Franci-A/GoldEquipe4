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
        
        int i = 0;
        int j = 0;
        foreach (GridLine line in grid)
        {
            foreach (Tile tile in line.line)
            {
                GameObject obj = Instantiate(tilePrefab, transform);
                obj.transform.localPosition = new Vector3(j * 1, i * 1, 0) - drawOffset;
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
                j++;
            }
            j = 0;
            i--;
        }
    }
}
