using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerPieceManager : MonoBehaviour
{
    private int currentTurn;
    [Header("Grids")]
    public List<Tile> grid;
    [SerializeField] private List<Tile> nextHand;

    [Header("House colors")]
    [SerializeField] private List<int> turnToLevelUpColors;
    [SerializeField] private int maxNumOfColors = 1;
    [SerializeField] private int maxNumOfHouses = 1;
    private int currentColorLevel = 0;

    [Header("Downgrade")]
    [SerializeField] private List<int> turnToLevelUpX;
    [SerializeField] private int maxNumOfX;
    [SerializeField] private float ChanceToGetX;
    private int currentXLevel = 0;

    [Header("Level up")]
    [SerializeField] private List<int> turnToLevelUpHammer;
    [SerializeField] private float ChanceToGetLevelUp;
    [SerializeField] private int maxNumOfLevelUp = 1;
    private int currentLevelUp = 0;


    [SerializeField] private SpriteLibrary sprites;

    private void Start()
    {
        UpdateVisual(grid);
        UpdateVisual(nextHand);
    }

    public void NextTurn()
    {
        //update hand
        for (int i = 0; i < grid.Count; i++) // move "next hand" to player hand
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

        List<int> maxValues = new List<int>();
        maxValues.Add(maxNumOfHouses);
        maxValues.Add(maxNumOfX);
        maxValues.Add(maxNumOfLevelUp);


        for (int b = 1; b < 4; b++) // get position for each object to be placed later on the new grid
        {
            int numOfObj = 0;
            if (b == 2)
            {
                float l = Random.Range(0, 1f);
                if( l < ChanceToGetX) { 
                    numOfObj = Random.Range(0, maxValues[b - 1] + 1);
                }
            }else if (b == 3)
            {
                float l = Random.Range(0, 1f);
                if (l < ChanceToGetLevelUp)
                {
                    numOfObj = Random.Range(0, maxValues[b - 1] + 1);
                }
            }
            else { 
                numOfObj = Random.Range(1, maxValues[b - 1] + 1);
            }

            for (int j = 0; j < numOfObj; j++)
            {
                int k;
                do
                {
                    k = Random.Range(0, 9);
                }
                while (type[k] != 0);
                type[k] = b;
            }
        }


        for (int i = 0; i < nextHand.Count; i++) // for each tile place the tile coresponding to the int that was given above
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
                case 3:
                    Debug.Log("Level up in hand");
                    nextHand[i].tileType = TileType.LevelUp;
                    nextHand[i].houseUpgrade = 0;
                    break;
            }
        }
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
        
        if(currentLevelUp < turnToLevelUpHammer.Count && currentTurn > turnToLevelUpX[currentLevelUp])
        {
            maxNumOfLevelUp++;
            currentLevelUp++;
        }
    }


    public void UpdateVisual(List<Tile> grid)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            SpriteRenderer obj = grid[i].transform.GetChild(0).GetComponent<SpriteRenderer>();

            if (grid[i].tileType == TileType.X)
            {
                obj.sprite = sprites.GetSprite("Bonus", "X");
                obj.color = Color.white;
            }
            else if(grid[i].tileType == TileType.LevelUp)
            {
                obj.sprite = sprites.GetSprite("Bonus", "LevelUp");
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
