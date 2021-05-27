using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Bonus_Malus : MonoBehaviour
{
    private GameObject currentCenterTile;
    [SerializeField] private GameObject snapImage;
    private bool bonusHeld;
    private bool haspos;
    bool isProtected;
    private BonusTile bonusTile;
    private Tile tileInfo;
    public SliderBar sliderBar;
    private Score score;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;
    private Vector3 snapPos;
    private Vector2 offset;

    public SpriteLibrary spriteLib;

    public List<bool> bonusesUsed;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>();
        bonusTile = GetComponent<BonusTile>(); 
        bonusTile.bonusType = BonusType.Chest;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Chest");
        resetPos = new Vector3(0,0,0);

        bonusesUsed = new List<bool>();
        for (int i = 0; i < 7; i++)
        {
            bonusesUsed.Add(false);
        }
        
    }

    void Update()
    {
        if (bonusHeld)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) + (Vector3)offset;

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
        if (bonusTile.bonusType != BonusType.Chest && !GameManager.Instance.gameOver)
        {
            bonusHeld = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
    }

    private void OnMouseUp()
    {
        bool isHouse = false;

        if (haspos && tileInfo.tileType == TileType.House && (bonusTile.bonusType == BonusType.Hammer1 || bonusTile.bonusType == BonusType.Hammer2 || bonusTile.bonusType == BonusType.Hammer3)) {
            snapImage.transform.position = snapPos;
            hammer();
            FindObjectOfType<AudioManager>().Play("Hammer");
            sliderBar.ChestImage.gameObject.GetComponent<Animator>().SetTrigger("Close");
            sliderBar.WaitToUpdateScore();

            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQAA"); // use a bonus achievement
        }

        if (haspos && bonusTile.bonusType == BonusType.Thunder && (tileInfo.tileType == TileType.House || tileInfo.tileType == TileType.Water))
        {
            if(tileInfo.tileType == TileType.House) {
                isHouse = true;
            }
            else {
                isHouse = false;
            }

            snapImage.transform.position = snapPos;
            thunder();
            sliderBar.ChestImage.gameObject.GetComponent<Animator>().SetTrigger("Close");
            sliderBar.WaitToUpdateScore();

            if (isHouse && isProtected == false) {

                tileInfo.scorePopup.sprite = tileInfo.sprites.GetSprite("Score", "-10");
                score.AddScore(-10);
            }

            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQBQ"); // use a malus achievement
        }

        if (haspos && bonusTile.bonusType == BonusType.Mountain && tileInfo.tileType == TileType.Ground) {
            snapImage.transform.position = snapPos;
            mountain();
            sliderBar.ChestImage.gameObject.GetComponent<Animator>().SetTrigger("Close");
            sliderBar.WaitToUpdateScore();

            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQBQ"); // use a malus achievement
        } 

        if (haspos && (bonusTile.bonusType == BonusType.Shield1 || bonusTile.bonusType == BonusType.Shield2) && tileInfo.tileType == TileType.House && tileInfo.shieldLvl < 2) {
            snapImage.transform.position = snapPos;
            shield();
            sliderBar.ChestImage.gameObject.GetComponent<Animator>().SetTrigger("Close");
            sliderBar.WaitToUpdateScore();

            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQAA"); // use a bonus achievement
        }

        bonusHeld = false;
        transform.localPosition = resetPos;
        snapImage.transform.localPosition = resetPos;
        bool canGetAchievement = true;
        foreach (bool item in bonusesUsed)
        {
            if (!item)
            {
                canGetAchievement = false;
                break;
            }
        }

        if (canGetAchievement)
        {
            Debug.Log("achievement");
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQCQ"); //catch em all bonuses
        }
        canGetAchievement = true;
        foreach (bool item in bonusesUsed)
        {
            if (item)
            {
                canGetAchievement = false;
                break;
            }
        }
        if(canGetAchievement && Score.Instance.currentScore >= 250 )
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQCg"); // score achievement
        }
        else if(canGetAchievement && Score.Instance.currentScore >= 500)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQCw"); // score achievement
        }
        else if(canGetAchievement && Score.Instance.currentScore >= 750)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQDA"); // score achievement
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            haspos = true;
            snapPos = collision.transform.position;
            currentCenterTile = collision.gameObject;
            tileInfo = currentCenterTile.GetComponent<Tile>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentCenterTile)
        {
            haspos = false;
        }
    }

    public void getBonus(int i) {
        if (i == 1) { 
            bonusTile.bonusType = BonusType.Hammer1;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer1");
        }
        else if (i == 2)
        {
            bonusTile.bonusType = BonusType.Hammer2;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer2");
        }
        else if (i == 3)
        {
            bonusTile.bonusType = BonusType.Hammer3;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Hammer3");
        }
        else if (i == 4)
        {
            bonusTile.bonusType = BonusType.Thunder;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Thunder");
        }
        else if (i == 5)
        {
            bonusTile.bonusType = BonusType.Mountain;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Mountain");
        }
        else if (i == 6)
        {
            bonusTile.bonusType = BonusType.Shield1;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Shield1");
        }
        else if (i == 7)
        {
            bonusTile.bonusType = BonusType.Shield2;
            snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Shield2");
        }
        bonusesUsed[i-1] = true;
    }

    void hammer()
    {
        if(bonusTile.bonusType == BonusType.Hammer1)
        {
            tileInfo.GetComponent<Animator>().SetFloat("MergeLevel", tileInfo.houseUpgrade);
            tileInfo.houseUpgrade++;
            tileInfo.GetComponent<Animator>().SetFloat("UpgradeNum", 1f);

        }
        else if (bonusTile.bonusType == BonusType.Hammer2)
        {
            tileInfo.GetComponent<Animator>().SetFloat("MergeLevel", tileInfo.houseUpgrade);
            tileInfo.houseUpgrade += 2;
            tileInfo.GetComponent<Animator>().SetFloat("UpgradeNum", 2f);
        }
        else if (bonusTile.bonusType == BonusType.Hammer3)
        {
            tileInfo.GetComponent<Animator>().SetFloat("MergeLevel", tileInfo.houseUpgrade);
            tileInfo.houseUpgrade += 3;
            tileInfo.GetComponent<Animator>().SetFloat("UpgradeNum", 3f);
        }
        if (tileInfo.houseUpgrade >= 4) {
            tileInfo.houseUpgrade = 4;
            tileInfo.scorePopup.sprite = tileInfo.sprites.GetSprite("Score", "+50");
            tileInfo.GetComponent<Animator>().SetFloat("UpgradeNum", 4f);
            tileInfo.GetComponent<Animator>().SetTrigger("FullUpgrade");
            score.AddScore(50);
        }
        else {
            
            tileInfo.GetComponent<Animator>().SetTrigger("Upgrade");
            tileInfo.GetComponent<Animator>().SetFloat("MergeRace", (int)tileInfo.houseColor);
            tileInfo.GetComponent<Merge>().merging();
        }

        transform.localPosition = resetPos;
        snapImage.transform.localPosition = resetPos;
        bonusTile.bonusType = BonusType.Chest;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Chest");
    }
    
    void thunder()
    {
        if (tileInfo.shieldLvl > 0)
        {
            tileInfo.shieldLvl -= 1;
            isProtected = true;
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQDw"); //200 volts achievement
            if (tileInfo.shieldLvl == 1) {
                tileInfo.GetComponent<Animator>().SetTrigger("ShieldUp2");

                tileInfo.scorePopup.sprite = tileInfo.sprites.GetSprite("Score", "0");
            }
            else if (tileInfo.shieldLvl == 0)
            {
                tileInfo.GetComponent<Animator>().SetTrigger("ShieldUp1");

                tileInfo.scorePopup.sprite = tileInfo.sprites.GetSprite("Score", "0");
            }
        }
        else
        {
            isProtected = false;
            tileInfo.GetComponent<Animator>().SetTrigger("Downgrade");
            tileInfo.scorePopup.sprite = tileInfo.sprites.GetSprite("Score", "0");
            tileInfo.houseUpgrade = 0;
            tileInfo.tileType = TileType.Ground;

        }

        transform.localPosition = resetPos;
        snapImage.transform.localPosition = resetPos;
        bonusTile.bonusType = BonusType.Chest;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Chest");
    }

    void mountain()
    {
        tileInfo.houseUpgrade = 0;
        tileInfo.tileType = TileType.Water;
        tileInfo.GetComponent<Animator>().SetTrigger("Mountain");
        transform.localPosition = resetPos;
        snapImage.transform.localPosition = resetPos;
        bonusTile.bonusType = BonusType.Chest;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Chest");
    } 

    void shield()
    {
        if (bonusTile.bonusType == BonusType.Shield1)
        {
            tileInfo.shieldLvl += 1;
        }
        else if (bonusTile.bonusType == BonusType.Shield2)
        {
            tileInfo.shieldLvl += 2;
        }
        if (tileInfo.shieldLvl > 2) 
        { 
            tileInfo.shieldLvl = 2; 
        }
        if (tileInfo.shieldLvl == 2)
        {
            tileInfo.GetComponent<Animator>().SetTrigger("ShieldUp2");
        }
        else if (tileInfo.shieldLvl == 1)
        {
            tileInfo.GetComponent<Animator>().SetTrigger("ShieldUp1");
        }

        transform.localPosition = resetPos;
        snapImage.transform.localPosition = resetPos;
        bonusTile.bonusType = BonusType.Chest;
        snapImage.GetComponent<SpriteRenderer>().sprite = spriteLib.GetSprite("Bonus", "Chest");
    }
}