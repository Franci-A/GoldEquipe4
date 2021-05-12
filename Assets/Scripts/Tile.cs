using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum TileType
{
    Ground,
    Water,
    Empty,
    House,
    X
}

public class Tile : MonoBehaviour
{
    public TileType tileType;
    public int houseUpgrade;
    public string houseColor;
    public int tileNum;
    public int lineNum;
}
