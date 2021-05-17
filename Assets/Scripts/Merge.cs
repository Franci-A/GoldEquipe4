using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    private Grid grid;
    private Score score;
    private Tile tileInfo;
    Tile rightTile;
    Tile leftTile;
    Tile upTile;
    Tile downTile;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
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
        int combo = 0;

        if (rightTile.tileType == TileType.House && rightTile.houseColor == tileInfo.houseColor && rightTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            rightTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            combo++;
            rightTile.tileType = TileType.Ground;
            grid.UpdateTile(rightTile.lineNum, rightTile.tileNum);
            merged = true;
        }

        if (leftTile.tileType == TileType.House && leftTile.houseColor == tileInfo.houseColor && leftTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            leftTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            combo++;
            leftTile.tileType = TileType.Ground;
            grid.UpdateTile(leftTile.lineNum, leftTile.tileNum);
            merged = true;
        }

        if (upTile.tileType == TileType.House && upTile.houseColor == tileInfo.houseColor && upTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            upTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            combo++;
            upTile.tileType = TileType.Ground;
            grid.UpdateTile(upTile.lineNum, upTile.tileNum);
            merged = true;
        }

        if (downTile.tileType == TileType.House && downTile.houseColor == tileInfo.houseColor && downTile.houseUpgrade == tileInfo.houseUpgrade)
        {
            downTile.houseUpgrade = 0;
            tempHouseUpgrade++;
            combo++;
            downTile.tileType = TileType.Ground;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
            merged = true;
        }

        if (combo == 2 && tileInfo.houseUpgrade == 1)
        {
            score.AddScore(10);
            Debug.Log("Doublé!");
        }

        tileInfo.houseUpgrade = tempHouseUpgrade;

        if(merged) {
            merging();
        }

        if (tileInfo.houseUpgrade >= 4)
        {
            tileInfo.tileType = TileType.Ground;
            tileInfo.houseUpgrade = 0;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
            score.AddScore(100);
        }

        if (combo == 3)
        {
            score.AddScore(20);
            Debug.Log("Triplé!");
        }

        if (combo == 4)
        {
            score.AddScore(30);
            Debug.Log("Quadruplé!");
        }

        combo = 0;
    }
}
