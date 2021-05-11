using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUpAndPlace : MonoBehaviour
{
    private bool isInHand;
    private Vector3 startPos;
    [SerializeField] private GameObject snapImage;
    [SerializeField] private Vector3 snapPos;
    private bool haspos;
    private GameObject currentCenterTile;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isInHand)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
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
        isInHand = true;
    }

    private void OnMouseUp()
    {
        isInHand = false;
        if (!haspos)
        {
            transform.position = startPos;
            snapImage.transform.position = transform.position;
        }
        else
        {
            transform.position = snapPos;
            snapImage.transform.position = transform.position;
        }
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
