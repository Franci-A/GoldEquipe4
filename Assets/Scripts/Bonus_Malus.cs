using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Malus : MonoBehaviour
{
    private GameObject currentCenterTile;
    public GameObject bonus;
    [SerializeField] private GameObject snapImage;
    private bool bonusHeld;
    private bool haspos;
    private bool canBePlaced;
    private BonusTile bonusTile;
    private Tile tileInfo;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;
    private Vector3 snapPos;
    private Vector2 offset;

    void Start()
    {
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
        bonusHeld = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    private void OnMouseUp()
    {
        bonusHeld = false;
        transform.position = resetPos;
        if (haspos && tileInfo.tileType == TileType.House) {
            snapImage.transform.position = snapPos;
            hammer();
        }
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

    void hammer()
    {
        int hammerLvl = Random.Range(1, 3);

        if (hammerLvl == 1) {
            tileInfo.houseUpgrade++;
        }
        if (hammerLvl == 2)
        {
            tileInfo.houseUpgrade += 2;
        }
        if (hammerLvl == 3)
        {
            tileInfo.houseUpgrade += 3;
        }

        tileInfo.UpdateVisual();
        snapImage.transform.position = resetPos;
    }
}