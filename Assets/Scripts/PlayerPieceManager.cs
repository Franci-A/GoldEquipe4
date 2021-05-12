using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieceManager : MonoBehaviour
{
    public List<Tile> grid;
    private int currentTurn;
    private List<Tile> nextHand;
    [SerializeField] private List<int> turnToLevelUp;
    private int numOfX;
    private int numOfHouses;
    private int numOfColors;

    public void NextTurn()
    {
        //update hand
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i] = nextHand[i];
        }
        //generate new next hand
        //update visual
        //update currentTurn
        currentTurn++;
    }
}
