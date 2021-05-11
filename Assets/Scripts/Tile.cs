using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum TileType
{
    Ground,
    Water,
    Empty
}

[Serializable]
public enum OnTile
{
    Empty,
    House,
    X
}

[Serializable]
public class Tile 
{
    public TileType tileType;
    public OnTile OnTile;
    public int houseUpgrade;
    public string houseColor;
}

[Serializable]
public class GridLine
{
    public List<Tile> line;
}
