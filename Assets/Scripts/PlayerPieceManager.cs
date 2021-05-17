using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerPieceManager : MonoBehaviour
{
    public List<Tile> grid;
    private int currentTurn;
    [SerializeField] private List<Tile> nextHand;
    [SerializeField] private List<int> turnToLevelUpColors;
    [SerializeField] private List<int> turnToLevelUpX;
    private int currentColorLevel = 0;
    private int currentXLevel = 0;
    [SerializeField] private int maxNumOfX;
    [SerializeField] private int maxNumOfHouses =1;
    [SerializeField] private int maxNumOfColors = 1;
    [SerializeField] private SpriteLibrary sprites;

    private void Start()
    {
        UpdateVisual(grid);
        UpdateVisual(nextHand);
    }

    public void NextTurn()
    {
        //update hand
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].tileType = nextHand[i].tileType;
            grid[i].houseUpgrade = nextHand[i].houseUpgrade;
            grid[i].houseColor = nextHand[i].houseColor;
        }
        //generate new next hand

        List<int> type = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            type.Add(0);
        }
        int numOfHouses = Random.Range(1, maxNumOfHouses + 1);
        for (int j = 0; j < numOfHouses; j++)
        {
            int k;
            do
            {
                k = Random.Range(0, 9);
            }
            while (type[k] != 0);
            type[k] = 1;
        }

        int numOfX = Random.Range(0, maxNumOfX + 1);
        for (int j = 0; j < numOfX; j++)
        {
            int k;
            do
            {
                k = Random.Range(0, 9);
            }
            while (type[k] != 0);
            type[k] = 2;
        }
        for (int i = 0; i < nextHand.Count; i++)
        {
            switch (type[i])
            {
                case 0:
                    nextHand[i].tileType = TileType.Empty;
                    nextHand[i].houseUpgrade = 0;
                    break;
                case 1:
                    nextHand[i].tileType = TileType.House;
                    nextHand[i].houseUpgrade = 1;
                    int l = Random.Range(0, maxNumOfColors);
                    nextHand[i].houseColor = (HouseColor)l;
                    break;
                case 2:
                    nextHand[i].tileType = TileType.X;
                    nextHand[i].houseUpgrade = 0;
                    break;
            }
        }
        //update visual
        UpdateVisual(grid);
        UpdateVisual(nextHand);
        //update currentTurn
        currentTurn++;

        if (currentColorLevel< turnToLevelUpColors.Count && currentTurn > turnToLevelUpColors[currentColorLevel])
        {
            maxNumOfColors++;
            maxNumOfHouses++;
            currentColorLevel++;
        }

        if(currentXLevel < turnToLevelUpX.Count && currentTurn > turnToLevelUpX[currentXLevel])
        {
            maxNumOfX++;
            currentXLevel++;
        }
    }


    public void UpdateVisual(List<Tile> grid)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            SpriteRenderer obj = grid[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

            if (grid[i].tileType == TileType.X)
            {
                obj.sprite = sprites.GetSprite("Red", "X");
                obj.color = Color.white;
            }
            else if (grid[i].tileType == TileType.House)
            {
                obj.color = Color.white;
                obj.sprite = sprites.GetSprite(grid[i].houseColor.ToString(), "level" + grid[i].houseUpgrade.ToString()); 
            }
            else
            {
                obj.color = Color.clear;
            }
        }
    }

}
