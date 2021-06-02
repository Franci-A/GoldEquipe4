using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.U2D.Animation;

public class Dragon : MonoBehaviour
{
    [SerializeField] private GameObject snapImage;
    public SpriteLibrary spriteLib;
    public Score score;
    public Grid grid;
    public Merge merge;
    bool dragonSpawned;
    bool dragonAttacked = true;
    int nbrTurn;
    int lastAttack = 0;
    int turnBeforeAttack = 6;
    public int minTurn = 15;
    public int minTurnNextAttack = 10;
    public int dragonChances = 20;
    public int targetNbr = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator timerAnimator;
    [SerializeField] private PIckUpAndPlace playerHand;

    public UnityEvent NextTurnEvent;

    public List<Tile> targetedTiles = new List<Tile>();

    void Start()
    {
        NextTurnEvent = GetComponentInChildren<PlayerPieceManager>().nextTurnEvent;
        NextTurnEvent.AddListener(SpawnDragon);
        NextTurnEvent.AddListener(TurnBeforeAttack);
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Dragon", "None");
    }

    public void SpawnDragon()
    {
        nbrTurn++;
        if (dragonAttacked) {
            lastAttack++;
        }

        if (nbrTurn >= minTurn && lastAttack >= minTurnNextAttack)
        {
            dragonAttacked = false;
            int random = Random.Range(1, dragonChances);

            if (random == 1 && !dragonSpawned)
            {
                animator.SetTrigger("Arrive");
                dragonSpawned = true;

                for (int i = 0; i < targetNbr; i++)
                {
                    int randomLineNum = Random.Range(1, 7);
                    int randomTileNum = Random.Range(1, 7);
                    bool alreadyInList = false;
                    foreach(Tile tile in targetedTiles)
                    {
                        if(grid.grid[grid.gridWidth * randomLineNum + randomTileNum] == tile)
                        {
                            alreadyInList = true;
                            break;
                        }
                    }
                    if(!alreadyInList)
                        targetedTiles.Add(grid.grid[grid.gridWidth * randomLineNum + randomTileNum]);
                }
            }
        }
    }

    void TurnBeforeAttack()
    {
        if(dragonSpawned == true)
        {
            timerAnimator.SetTrigger("NextStep");
            turnBeforeAttack = turnBeforeAttack - 1;
            if(turnBeforeAttack == 0)
            {
                playerHand.BlockHandForSec(2.0f);
                animator.SetTrigger("Attack");
            }
        }
    }
    public void DragonAttack()
    {
        int touched = 0;
        foreach (Tile tiles in targetedTiles)
        {
            if (tiles.tileType == TileType.House) {
                touched++;
                if (tiles.shieldLvl == 2) {
                    tiles.GetComponent<Animator>().SetTrigger("ShieldPop2");
                    tiles.shieldLvl--;
                    AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQDg");
                }
                else if (tiles.shieldLvl == 1)
                {
                    tiles.GetComponent<Animator>().SetTrigger("ShieldPop1");
                    tiles.shieldLvl--;
                    AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQDg");
                }
                else
                {
                    tiles.houseUpgrade--;
                    tiles.scorePopup.sprite = spriteLib.GetSprite("Score", "0");
                    tiles.GetComponent<Animator>().SetTrigger("Downgrade");
                }
                if(tiles.houseUpgrade == 0) {
                    tiles.scorePopup.sprite = spriteLib.GetSprite("Score", "-10");
                    tiles.tileType = TileType.Ground;
                    score.AddScore(-10);
                }
            }

            else if (tiles.tileType == TileType.Water)
            {
                touched++;
                tiles.tileType = TileType.Ground;
                tiles.scorePopup.sprite = spriteLib.GetSprite("Score", "0");
                tiles.GetComponent<Animator>().SetTrigger("Downgrade");
            }
        }

        if (touched > 0) {
            VibrationManager.Instance.isFiled = true;
        }
        else {
            VibrationManager.Instance.isFiled = false;
        }

        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Dragon", "None");
        turnBeforeAttack = 6;
        dragonSpawned = false;
        dragonAttacked = true;
        lastAttack = 0;
        targetedTiles.Clear();
    }
}
