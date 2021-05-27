using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.U2D.Animation;

public class Dragon : MonoBehaviour
{
    [SerializeField] private GameObject snapImage;
    public SpriteLibrary spriteLib;
    private Tile tileInfo;
    public Grid grid;
    bool dragonSpawned;
    int nbrTurn;
    int turnBeforeAttack = 6;
    public int minTurn = 15;
    public int dragonChances = 20;
    public int targetNbr = 5;

    public UnityEvent NextTurnEvent;

    public List<Tile> targetedTiles = new List<Tile>();

    void Start()
    {
        NextTurnEvent = GetComponentInChildren<PlayerPieceManager>().nextTurnEvent;
        NextTurnEvent.AddListener(SpawnDragon);
        NextTurnEvent.AddListener(TurnBeforeAttack);
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Dragon", "None");
    }

    void SpawnDragon()
    {
        nbrTurn++;
        if (nbrTurn >= minTurn)
        {
            int random = Random.Range(1, dragonChances);

            if (random == 1)
            {
                snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Dragon", "Dragon");
                dragonSpawned = true;

                for (int i = 0; i < targetNbr; i++)
                {
                    int randomLineNum = Random.Range(1, 7);
                    int randomTileNum = Random.Range(1, 7);
                    targetedTiles.Add(grid.grid[grid.gridWidth * randomLineNum + randomTileNum]);
                    targetedTiles[i].targeted.sprite = spriteLib.GetSprite("Target", "Target");
                }
            }
        }
    }

    void TurnBeforeAttack()
    {
        if(dragonSpawned == true)
        {
            turnBeforeAttack = turnBeforeAttack - 1;
            Debug.Log(turnBeforeAttack);
            if(turnBeforeAttack == 0)
            {
                DragonAttack();
            }
        }
    }
    void DragonAttack()
    {
        foreach(Tile tiles in targetedTiles)
        {
            tiles.targeted.sprite = spriteLib.GetSprite("Target", "None");
            if (tiles.tileType == TileType.House) {
                if(tiles.shieldLvl == 2) {
                    tiles.GetComponent<Animator>().SetTrigger("ShieldPop2");
                    tiles.shieldLvl--;
                }
                if (tiles.shieldLvl == 1)
                {
                    tiles.GetComponent<Animator>().SetTrigger("ShieldPop1");
                    tiles.shieldLvl--;
                }
                else
                {
                    tiles.houseUpgrade--;
                    tiles.GetComponent<Animator>().SetTrigger("Downgrade");
                }
                if(tiles.houseUpgrade == 0) {
                    tiles.tileType = TileType.Ground;
                }
            }

            if (tiles.tileType == TileType.Water)
            {
                tiles.tileType = TileType.Ground;
                tiles.scorePopup.sprite = spriteLib.GetSprite("Score", "0");
                tiles.GetComponent<Animator>().SetTrigger("Downgrade");
            }
        }
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Dragon", "None");
        turnBeforeAttack = 6;
        dragonSpawned = false;
    }
}
