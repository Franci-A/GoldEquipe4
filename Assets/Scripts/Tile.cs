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
    public int shieldLvl;
    public HouseColor houseColor;
    public int tileNum;
    public int lineNum;
    public SpriteLibrary sprites;
    public SpriteRenderer scorePopup;
    public ParticleSystem destroyParticles;
    public SpriteRenderer shieldSprite;
    public Animator targeted;

    public void UpdateVisual()
    {
        SpriteRenderer obj = transform.GetChild(0).GetComponent<SpriteRenderer>();


        switch (tileType)
        {
            case TileType.House:
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level" + houseUpgrade.ToString());
                destroyParticles.textureSheetAnimation.SetSprite(0, sprites.GetSprite("Particles", houseColor.ToString() + 1));
                destroyParticles.textureSheetAnimation.SetSprite(1, sprites.GetSprite("Particles", houseColor.ToString() + 2));
                destroyParticles.textureSheetAnimation.SetSprite(2, sprites.GetSprite("Particles", houseColor.ToString() + 3));
                destroyParticles.textureSheetAnimation.SetSprite(3, sprites.GetSprite("Particles", houseColor.ToString() + 4));
                destroyParticles.textureSheetAnimation.SetSprite(4, sprites.GetSprite("Particles", houseColor.ToString() + 5));
                break;
            case TileType.LevelUp:
                obj.sprite = sprites.GetSprite("Bonus", "Hammer1");
                break;
            case TileType.X:
                obj.sprite = sprites.GetSprite("Bonus", "Thunder");
                break;
            case TileType.Water:
                int i = UnityEngine.Random.Range(1, 4);
                obj.sprite = sprites.GetSprite("Tiles", "Water" + i);
                destroyParticles.textureSheetAnimation.SetSprite(0, sprites.GetSprite("Particles", "Mountain1"));
                destroyParticles.textureSheetAnimation.SetSprite(1, sprites.GetSprite("Particles", "Mountain2"));
                destroyParticles.textureSheetAnimation.SetSprite(2, sprites.GetSprite("Particles", "Mountain3"));
                destroyParticles.textureSheetAnimation.SetSprite(3, sprites.GetSprite("Particles", "Mountain4"));
                destroyParticles.textureSheetAnimation.SetSprite(4, sprites.GetSprite("Particles", "Mountain5"));
                break;
            default:
                obj.sprite = sprites.GetSprite(houseColor.ToString(), "level0");
                break;

        }

         shieldSprite.sprite = sprites.GetSprite("Shield", "Shield" + shieldLvl);

        if(houseUpgrade >= 4)
        {
            houseUpgrade = 0;
            tileType = TileType.Ground;
        }
    }

    public void playSound(string name) {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void playSoundWithoutStr()
    {
        FindObjectOfType<AudioManager>().PlayWithoutStr();
    }

    public void playDestruction()
    {
        if (houseUpgrade == 0) {
            FindObjectOfType<AudioManager>().Play("Destruction");
        }
    }
}
