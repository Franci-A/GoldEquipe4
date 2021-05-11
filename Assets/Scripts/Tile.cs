using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum TileType
{
    Ground,
    Water
}

[Serializable]
public enum OnTile
{
    Empty,
    House,
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
