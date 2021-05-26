using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftyFlip : MonoBehaviour
{
    [SerializeField] private GameObject nextGrid;
    [SerializeField] private GameObject bonusChest;

    private void Start()
    {
        if( PlayerPrefs.GetInt("LeftFlip") == 1) //LeftFlip = 0 => nextGrid on right || LeftFlip = 1 => NextGrid on left 
            FlipItems();
    }

    public void FlipItems()
    {
        Vector3 tempPosition = nextGrid.transform.position;
        nextGrid.transform.position = bonusChest.transform.position;
        bonusChest.transform.position = tempPosition;
    }
}
