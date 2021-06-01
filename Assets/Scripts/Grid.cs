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
    public bool existingGrid;
    [SerializeField] private float waterTileSpawnChance;
    [SerializeField] private int maxWaterTiles;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float cellSize;
    public Vector3 drawOffset;
    private SpriteLibrary sprites;
    [SerializeField] PlayerPieceManager playerPieceManager;


    private void Start()
    {
        drawOffset = new Vector3(tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x * gridWidth / 2 + tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, drawOffset.y, 0);
        sprites = GetComponent<SpriteLibrary>();
        gridHeight += 2;
        gridWidth += 2;

        if (grid.Count == 0)
        {
            InstanciateGrid();
            while (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x > grid[0].gameObject.transform.position.x - grid[0].GetComponent<SpriteRenderer>().bounds.size.x / 4)
            {
                Camera.main.orthographicSize += .1f;
            }
        }
        else if (!existingGrid)
        {
            int x = 0;
            int y = 0;
            foreach (Tile tile in grid)
            {
                tile.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
                tile.lineNum = y;
                tile.tileNum = x;
                tile.UpdateVisual();
                x++;
                if (x == gridWidth)
                {
                    x = 0;
                    y++;
                }
            }
        }else if (existingGrid)
        {
            UpdateCurrentGrid();
        }
    }

    public void UpdateTile(int line, int tile)
    {
        SpriteRenderer obj = grid[gridWidth * line + tile].transform.GetChild(0).GetComponent<SpriteRenderer>();
        Tile objTile = obj.gameObject.GetComponentInParent<Tile>();
        obj.sprite = sprites.GetSprite("House", "level" + grid[gridWidth * line + tile].houseUpgrade.ToString());
        obj.sprite = sprites.GetSprite(objTile.houseColor.ToString(), "level" + objTile.houseUpgrade.ToString());

    }

    //Create grid 
    public void InstanciateGrid()
    {
        int x = 0;
        int y = 0;
        int mountainX = Random.Range(1, 7);
        int mountainY = Random.Range(1, 7);
        for (int i = 0; i < (gridHeight) * (gridWidth); i++)
        {
            Tile obj = Instantiate(tilePrefab, transform).GetComponent<Tile>();
            grid.Add(obj);
            cellSize = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            obj.gameObject.transform.position = new Vector3(x * cellSize, y * -1 * cellSize, 0) - drawOffset;
            obj.lineNum = y;
            obj.tileNum = x;
            obj.GetComponent<SpriteRenderer>().sortingOrder = y;
            obj.GetComponent<Merge>().pIckUpAndPlace = playerPieceManager.gameObject.GetComponent<PIckUpAndPlace>();
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = y + 9;
            if (y == 0 || y == gridHeight - 1 || x == 0 || x == gridWidth - 1) // outer ring instantiate to empty tile
                obj.tileType = TileType.Empty;
            else if (randomGen)
            {
                float k = Random.Range(0, 1f);
                obj.GetComponent<EnvironmentChanges>().NextTurnEvent = playerPieceManager.nextTurnEvent;
                if ( maxWaterTiles > 0 && ((x == mountainX && y == mountainY ) || k < waterTileSpawnChance ))
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
                    int j = Random.Range(1, 3);
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Water" +j);
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


    public void UpdateCurrentGrid()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < (gridHeight) * (gridWidth); i++)
        {
            grid[i].lineNum = y;
            grid[i].tileNum = x;
            grid[i].GetComponent<SpriteRenderer>().sortingOrder = y;
            grid[i].GetComponent<Merge>().pIckUpAndPlace = playerPieceManager.gameObject.GetComponent<PIckUpAndPlace>();
            grid[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = y + 9;
            if (y == 0 || y == gridHeight - 1 || x == 0 || x == gridWidth - 1) // outer ring instantiate to empty tile
                grid[i].tileType = TileType.Empty;
            switch (grid[i].tileType)
            {
                case TileType.Ground:
                    grid[i].GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Ground");
                    break;

                case TileType.Water:
                    grid[i].GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Ground");
                    int j = Random.Range(1, 3);
                    grid[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Tiles", "Water" + j);
                    grid[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    break;

                case TileType.Empty:
                    grid[i].GetComponent<SpriteRenderer>().color = Color.clear;
                    break;
            }
            x++;
            if (x >= gridWidth)
            {
                y++;
                x = 0;
            }
            grid[i].UpdateVisual();
            grid[i].GetComponent<Merge>().tileInfo = grid[i];
            grid[i].GetComponent<Merge>().grid = this;
        }
        for (int i = 0; i < (gridHeight) * (gridWidth); i++)
        {
            grid[i].GetComponent<Merge>().UpdateTiles();
        }

    }
    
}
