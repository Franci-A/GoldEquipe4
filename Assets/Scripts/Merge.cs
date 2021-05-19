using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    [SerializeField] private Combo_DB comboData;
    private Grid grid;
    private Score score;
    private Tile tileInfo;
    Tile rightTile;
    Tile leftTile;
    Tile upTile;
    Tile downTile;
    int combo;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        grid = GetComponentInParent<Grid>();
        tileInfo = GetComponent<Tile>();
        if (tileInfo.tileType != TileType.Empty) {
            rightTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum + 1].GetComponent<Tile>();
            leftTile = grid.grid[grid.gridWidth * tileInfo.lineNum + tileInfo.tileNum - 1].GetComponent<Tile>();
            upTile = grid.grid[grid.gridWidth * (tileInfo.lineNum - 1) + tileInfo.tileNum].GetComponent<Tile>();
            downTile = grid.grid[grid.gridWidth * (tileInfo.lineNum + 1) + tileInfo.tileNum].GetComponent<Tile>();
        }
    }
    public void merging()
    {
        int tempHouseUpgrade = tileInfo.houseUpgrade;
        bool merged = false;
        combo = 0;

        if (rightTile.tileType == TileType.House && rightTile.houseColor == tileInfo.houseColor && rightTile.houseUpgrade == tempHouseUpgrade)
        {
            rightTile.GetComponent<Animator>().SetTrigger("OnRight");
            rightTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            rightTile.tileType = TileType.Ground;
            merged = true;
        }

        if (leftTile.tileType == TileType.House && leftTile.houseColor == tileInfo.houseColor && leftTile.houseUpgrade == tempHouseUpgrade)
        {
            leftTile.GetComponent<Animator>().SetTrigger("OnLeft");
            leftTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            leftTile.tileType = TileType.Ground;
            merged = true;
        }

        if (upTile.tileType == TileType.House && upTile.houseColor == tileInfo.houseColor && upTile.houseUpgrade == tempHouseUpgrade)
        {
            upTile.GetComponent<Animator>().SetTrigger("OnTop");
            upTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            upTile.tileType = TileType.Ground;
            merged = true;
        }

        if (downTile.tileType == TileType.House && downTile.houseColor == tileInfo.houseColor && downTile.houseUpgrade == tempHouseUpgrade)
        {
            downTile.GetComponent<Animator>().SetTrigger("OnBottom");
            downTile.houseUpgrade = 0;
            tileInfo.houseUpgrade++;
            combo++;
            downTile.tileType = TileType.Ground;
            merged = true;
        }


        if (combo >= 2) {
            comboValue(tempHouseUpgrade);
        }

        if (tileInfo.houseUpgrade >= 4 &&  combo < 2)
        {
            tileInfo.houseUpgrade = 4;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = tileInfo.sprites.GetSprite(tileInfo.houseColor.ToString(), "level4");
            this.GetComponent<Animator>().SetTrigger("FullUpgrade");
            StartCoroutine(UpdateVisuelAtFullUpgrade());
            tileInfo.tileType = TileType.Ground;
            tileInfo.houseUpgrade = 0;
            score.AddScore(50);
        }

        if (merged && tileInfo.houseUpgrade < 4)
        {
            merging();
            this.GetComponent<Animator>().SetTrigger("Upgrade");
        }

    }
    void comboValue(int tempHouseUpgrade)
    {
        int bonusScore = 0;
        switch (tempHouseUpgrade) {
            case 1:
                if (combo == 2)
                {
                    return;
                }
                if (combo == 3) {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUAD_LVL1)._value;
                }
                else if (combo == 4) {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUINT_LVL1)._value;
                }
                break;
            case 2:
                if (combo == 2)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.TRIPLE_LVL2)._value;
                }
                else if (combo == 3)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUAD_LVL2)._value;
                }
                else if (combo == 4)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUINT_LVL2)._value;
                }
                break;
            case 3:
                if (combo == 2)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.TRIPLE_LVL3)._value;
                }
                else if (combo == 3)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUAD_LVL3)._value;
                }
                else if (combo == 4)
                {
                    bonusScore = comboData._comboConfig.Find(x => x._typeCombo == Combo_DB.COMBO_TYPE.QUINT_LVL3)._value;
                }
                break;
        }
        score.AddScore(bonusScore);

        tileInfo.tileType = TileType.Ground;
        tileInfo.houseUpgrade = 0;
    }

    public void PLayParticles()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    IEnumerator UpdateVisuelAtFullUpgrade()
    {
        yield return new WaitForSeconds(1.3f);
        tileInfo.UpdateVisual();
    }
}
