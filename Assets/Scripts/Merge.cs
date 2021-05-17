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
    int combo;

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
        combo = 0;

        if (rightTile.tileType == TileType.House && rightTile.houseColor == tileInfo.houseColor && rightTile.houseUpgrade == tempHouseUpgrade)
        {
            rightTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            rightTile.tileType = TileType.Ground;
            grid.UpdateTile(rightTile.lineNum, rightTile.tileNum);
            merged = true;
        }

        if (leftTile.tileType == TileType.House && leftTile.houseColor == tileInfo.houseColor && leftTile.houseUpgrade == tempHouseUpgrade)
        {
            leftTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            leftTile.tileType = TileType.Ground;
            grid.UpdateTile(leftTile.lineNum, leftTile.tileNum);
            merged = true;
        }

        if (upTile.tileType == TileType.House && upTile.houseColor == tileInfo.houseColor && upTile.houseUpgrade == tempHouseUpgrade)
        {
            upTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            upTile.tileType = TileType.Ground;
            grid.UpdateTile(upTile.lineNum, upTile.tileNum);
            merged = true;
        }

        if (downTile.tileType == TileType.House && downTile.houseColor == tileInfo.houseColor && downTile.houseUpgrade == tempHouseUpgrade)
        {
            downTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            downTile.tileType = TileType.Ground;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
            merged = true;
        }

        if (merged  && tileInfo.houseUpgrade < 4) {
            merging();
        }

        if (combo >= 2) {
            comboValue();
        }

        if (tileInfo.houseUpgrade >= 4 &&  combo < 2)
        {
            tileInfo.tileType = TileType.Ground;
            tileInfo.houseUpgrade = 0;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
            score.AddScore(25);
        }

        void comboValue()
        {
            int bonusScore;
            int comboPenalty = 0;
            if(tempHouseUpgrade == 1) {
                comboPenalty = 10;
            }
            if (tempHouseUpgrade == 2)
            {
                comboPenalty = 5;
            }
            if (tempHouseUpgrade == 3)
            {
                comboPenalty = 0;
            }
            bonusScore = combo * 5 - comboPenalty;
            score.AddScore(bonusScore);
            tileInfo.tileType = TileType.Ground;
            tileInfo.houseUpgrade = 0;
            grid.UpdateTile(downTile.lineNum, downTile.tileNum);
        } 
    }
}
