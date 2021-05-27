using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerPieceManager : MonoBehaviour
{
    private int currentTurn =1;
    [Header("Grids")]
    public List<Tile> grid;
    [SerializeField] private List<Tile> nextHand;

    [Header("Faction")]
    [SerializeField] private List<int> turnToLevelUpColors;
    [SerializeField] private int maxNumOfColors = 1;
    private int currentColorLevel = 0;
    
    [Header("Houses")]
    [SerializeField] private List<int> turnToLevelUpHouses;
    [SerializeField] private int maxNumOfHouses = 1;
    private int currentHouses = 0;

    [Header("Downgrade")]
    [SerializeField] private List<int> turnToLevelUpX;
    [SerializeField] private int maxNumOfX;
    [SerializeField] private float ChanceToGetX;
    private int currentXLevel = 0;


    [SerializeField] private SpriteLibrary sprites;
    public UnityEngine.Events.UnityEvent nextTurnEvent;

    private void Start()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i].UpdateVisual();
        }
        for (int i = 0; i < nextHand.Count; i++)
        {
            nextHand[i].UpdateVisual();
        }

    }

    public void NextTurn()
    {
        //update hand
        for (int i = 0; i < grid.Count; i++) // move "next hand" to player hand
        {
            grid[i].tileType = nextHand[i].tileType;
            grid[i].houseUpgrade = nextHand[i].houseUpgrade;
            grid[i].houseColor = nextHand[i].houseColor;
            grid[i].UpdateVisual();
        }
        //generate new next hand

        List<int> type = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            type.Add(0);
        }



        for (int b = 1; b < 2; b++) // get position for each object to be placed later on the new grid
        {
            int numOfObj = 0;
            if (b == 2)
            {
                float l = Random.Range(0, 1f);
                if( l < ChanceToGetX) { 
                    numOfObj = Random.Range(0, maxNumOfX + 1);
                }
            }
            else { 
                numOfObj =maxNumOfHouses/*Random.Range(1, maxValues[b - 1] + 1)*/;
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
                    nextHand[i].tileType = TileType.LevelUp;
                    nextHand[i].houseUpgrade = 0;
                    break;
            }
            nextHand[i].UpdateVisual();
        }
        //update currentTurn
        currentTurn++;
        nextTurnEvent.Invoke();

        if (currentColorLevel< turnToLevelUpColors.Count && currentTurn > turnToLevelUpColors[currentColorLevel])
        {
            maxNumOfColors++;
            currentColorLevel++;
        }

        if(currentXLevel < turnToLevelUpX.Count && currentTurn > turnToLevelUpX[currentXLevel])
        {
            maxNumOfX++;
            currentXLevel++;
        }
        
        if(currentHouses < turnToLevelUpHouses.Count && currentTurn > turnToLevelUpHouses[currentHouses])
        {
            maxNumOfHouses++;
            currentHouses++;
        }
    }

}
