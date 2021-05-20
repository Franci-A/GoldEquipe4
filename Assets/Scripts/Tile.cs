using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI scorePopup;

    public void UpdateVisual()
    {
        SpriteRenderer obj = transform.GetChild(0).GetComponent<SpriteRenderer>();


        switch (tileType)
        {
            case TileType.House:
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level" + houseUpgrade.ToString());
                break;
            case TileType.LevelUp:
                obj.sprite = sprites.GetSprite("Bonus", "Hammer1");
                break;
            case TileType.X:
                obj.sprite = sprites.GetSprite("Bonus", "Thunder");
                break;
            case TileType.Water:
                obj.sprite = sprites.GetSprite("Tiles", "Water");
                break;
            default:
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level0");
                break;

        }
        if(houseUpgrade >= 4)
        {
            houseUpgrade = 0;
            tileType = TileType.Ground;
        }
    }

}
