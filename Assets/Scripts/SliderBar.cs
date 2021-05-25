using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Bonus_Malus bonusM;
    public SpriteRenderer ChestImage;
    public int getBonusValue;
    private Score score;
    private int backScore;
    private int random;
    private int i = 1;
    public int tempScore;
    public bool haveBonus;

    void Start()
    {
        score = GetComponent<Score>();
        tempScore = 0;
        haveBonus = false;
    }
    void Update()
    {
        if (!haveBonus)
        {
            backScore = score.currentScore - tempScore;
            //ChestImage.
        }
        if (backScore >= getBonusValue && haveBonus == false)
        {
            random = Random.Range(1, 8);
            bonusM.getBonus(random);
            haveBonus = true;
            FindObjectOfType<AudioManager>().Play("GetBonus");
        }
    }
}

