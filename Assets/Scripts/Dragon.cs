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
        Debug.Log(lastAttack);
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
        foreach (Tile tiles in targetedTiles)
        {
            if (tiles.tileType == TileType.House) {
                if(tiles.shieldLvl == 2) {
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
                    tiles.GetComponent<Animator>().SetTrigger("Downgrade");
                }
                if(tiles.houseUpgrade == 0) {
                    tiles.tileType = TileType.Ground;
                    score.AddScore(-10);
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
        dragonAttacked = true;
        lastAttack = 0;
        targetedTiles.Clear();
    }
}
