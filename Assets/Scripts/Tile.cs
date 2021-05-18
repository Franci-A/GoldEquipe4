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
    X
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
    [SerializeField] SpriteLibrary sprites;

    public void UpdateVisual()
    {
            SpriteRenderer obj = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (tileType == TileType.X)
        {
            obj.sprite = sprites.GetSprite("Red", "X");
            obj.color = Color.white;
        }
        else if (tileType == TileType.House)
        {
            obj.color = Color.white;
            obj.sprite = sprites.GetSprite(houseColor.ToString(), "level" + houseUpgrade.ToString());
        }
        else
        {
            obj.color = Color.clear;
        }
        
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
