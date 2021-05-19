using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

[Serializable]
public enum TileType
{
    Ground,
    Water,
    Empty,
    House,
    X,
    LevelUp
}

public enum HouseColor
{
    Blue,
    Green,
    Yellow,
    Red    
}

public class Tile : MonoBehaviour
{
    public TileType tileType;
    public int houseUpgrade;
    public HouseColor houseColor;
    public int tileNum;
    public int lineNum;
    public SpriteLibrary sprites;

    public void UpdateVisual()
    {
        SpriteRenderer obj = transform.GetChild(0).GetComponent<SpriteRenderer>();


        switch (tileType)
        {
            case TileType.House:
                obj.color = Color.white;
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level" + houseUpgrade.ToString());
                break;

            default:
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level0");
                obj.color = Color.clear;
                break;

        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
