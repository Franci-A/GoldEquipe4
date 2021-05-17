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

public enum HouseColor
{
    Red,
    Green,
    Blue,
    Yollow
}

public class Tile : MonoBehaviour
{
    public TileType tileType;
    public int houseUpgrade;
    public HouseColor houseColor;
    public int tileNum;
    public int lineNum;
}
