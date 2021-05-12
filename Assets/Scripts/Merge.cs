using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour {

    [SerializeField] private Grid grid;
    [SerializeField] private Tile tileInfo;
    Tile rightTile;
    Tile leftTile;
    Tile upTile;
    Tile downTile;

    void Start() {
        grid = GetComponentInParent<Grid>();
        tileInfo = GetComponent<Tile>();
        rightTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum + 1].GetComponent<Tile>();
        leftTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum - 1].GetComponent<Tile>();
        upTile = grid.grid[grid.gridWidth * (tileInfo.lineNum - 1) + tileInfo.tileNum].GetComponent<Tile>();
        downTile = grid.grid[grid.gridWidth * (tileInfo.lineNum + 1) + tileInfo.tileNum].GetComponent<Tile>();
    }

    public void merging() {
    }
}
