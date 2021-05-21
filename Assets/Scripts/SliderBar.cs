using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Bonus_Malus bonusM;
    public Slider ScoreBar;
    public Score score;
    public int backScore;
    public int random;
    public int i = 1;
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
            ScoreBar.value = backScore;
        }
        if (backScore >= (ScoreBar.maxValue) && haveBonus == false)
        {
            random = Random.Range(1, 4);
            bonusM.getBonus(random);
            haveBonus = true;
        }
    }
}

