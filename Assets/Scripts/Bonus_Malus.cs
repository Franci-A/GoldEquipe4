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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            haspos = true;
            snapPos = collision.transform.position;
            currentCenterTile = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentCenterTile)
        {
            haspos = false;
        }
    }
}