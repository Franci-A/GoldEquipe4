using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    private Grid grid;
    private Tile tileInfo;
    Tile rightTile;
    Tile leftTile;
    Tile upTile;
    Tile downTile;

    void Start()
    {
        grid = GetComponentInParent<Grid>();
        tileInfo = GetComponent<Tile>();
        if (tileInfo.tileType != TileType.Empty) {
            rightTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum + 1].GetComponent<Tile>();
            leftTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum - 1].GetComponent<Tile>();
            upTile = grid.grid[grid.gridWidth * (tileInfo.lineNum - 1) + tileInfo.tileNum].GetComponent<Tile>();
            downTile = grid.grid[grid.gridWidth * (tileInfo.lineNum + 1) + tileInfo.tileNum].GetComponent<Tile>();
        }
    }

    public void merging()
    {
        int tempHouseUpgrade = tileInfo.houseUpgrade;
        bool merged = false;

        if (rightTile.tileType == TileType.House && rightTile.houseColor == tileInfo.houseColor && rightTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            rightTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            rightTile.tileType = TileType.Ground;
            grid.UpdateTile(rightTile.lineNum, rightTile.tileNum);
            merged = true;
        }

        if (leftTile.tileType == TileType.House && leftTile.houseColor == tileInfo.houseColor && leftTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            leftTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            leftTile.tileType = TileType.Ground;
            grid.UpdateTile(leftTile.lineNum, leftTile.tileNum);
            merged = true;
        }

        if (upTile.tileType == TileType.House && upTile.houseColor == tileInfo.houseColor && upTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            upTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            upTile.tileType = TileType.Ground;
            grid.UpdateTile(upTile.lineNum, upTile.tileNum);
            merged = true;
        }

        if (downTile.tileType == TileType.House && downTile.houseColor == tileInfo.houseColor && downTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            downTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            downTile.tileType = TileType.Ground;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
            merged = true;
        }

        tileInfo.houseUpgrade = tempHouseUpgrade;

        if(merged) {
            merging();
        }
    }
}
