using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Bonus_Malus : MonoBehaviour
{
    private GameObject currentCenterTile;
    [SerializeField] private GameObject snapImage;
    private bool bonusHeld;
    private bool haspos;
    private BonusTile bonusTile;
    private Tile tileInfo;
    private SliderBar sliderBar;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;
    private Vector3 snapPos;
    private Vector2 offset;

    public SpriteLibrary spriteLib;

    void Start()
    {
        bonusTile = GetComponent<BonusTile>(); 
        bonusTile.bonusType = BonusType.Hammer3;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer3");
        resetPos = transform.position;
    }

    void Update()
    {
        if (bonusHeld)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) + (Vector3)offset;

            if (haspos)
            {
                snapImage.transform.position = snapPos;
            }
            else
            {
                snapImage.transform.position = transform.position;
            }
        }
    }

    private void OnMouseDown()
    {
        if (bonusTile.bonusType != BonusType.Chest)
        {
            bonusHeld = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
    }

    private void OnMouseUp()
    {
        if (haspos && bonusTile.bonusType == BonusType.Hammer1 || bonusTile.bonusType == BonusType.Hammer2 || bonusTile.bonusType == BonusType.Hammer3) {
            snapImage.transform.position = snapPos;
            hammer();
        }

        bonusHeld = false;
        transform.position = resetPos;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            haspos = true;
            snapPos = collision.transform.position;
            currentCenterTile = collision.gameObject;
            tileInfo = currentCenterTile.GetComponent<Tile>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentCenterTile)
        {
            haspos = false;
        }
    }

    public void getBonus(int i) {
        if (i == 1) { 
            bonusTile.bonusType = BonusType.Hammer1;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer1");
        }
        if (i == 2)
        {
            bonusTile.bonusType = BonusType.Hammer2;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer2");
        }
        if (i == 3)
        {
            bonusTile.bonusType = BonusType.Hammer3;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer3");
        }
        if (i == 4)
        {
            bonusTile.bonusType = BonusType.Thunder;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Thunder");
        }
        if (i == 5)
        {
            bonusTile.bonusType = BonusType.Montain;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Montain");
        }
        if (i == 6)
        {
            bonusTile.bonusType = BonusType.Shield1;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Shield1");
        }
        if (i == 7)
        {
            bonusTile.bonusType = BonusType.Shield2;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Shield2");
        }
    }

    void hammer()
    {
        if(bonusTile.bonusType == BonusType.Hammer1) {
            tileInfo.houseUpgrade++;
        }
        if (bonusTile.bonusType == BonusType.Hammer2)
        {
            tileInfo.houseUpgrade += 2;
        }
        if (bonusTile.bonusType == BonusType.Hammer3)
        {
            tileInfo.houseUpgrade += 3;
        }
        tileInfo.UpdateVisual();
        snapImage.transform.position = resetPos;
    }
}