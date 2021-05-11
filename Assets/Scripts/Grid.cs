using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public List<GridLine> grid;
    [SerializeField] private GameObject tilePrefab;

    private void Start()
    {
        int i = 0;
        int j = 0;
        foreach (GridLine line in grid)
        {
            foreach (Tile tile in line.line)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector3(j * 1, i * 1, 0), transform.rotation);
                if(tile.tileType == TileType.Ground)
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                j++;
            }
            j = 0;
            i--;
        }
    }
}
