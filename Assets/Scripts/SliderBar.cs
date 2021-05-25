using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Bonus_Malus bonusM;
    public Image ChestImage;
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
            ChestImage.fillAmount = ((backScore* 100.0f) / getBonusValue)/ 100.0f;
        }
        if (backScore >= getBonusValue && haveBonus == false)
        {
            ChestImage.gameObject.GetComponent<Animator>().SetTrigger("Open");
            random = Random.Range(1, 8);
            bonusM.getBonus(random);
            haveBonus = true;
            FindObjectOfType<AudioManager>().Play("GetBonus");
        }
    }

    public void WaitToUpdateScore()
    {
        StartCoroutine(SetNewScoreValue());
    }

    IEnumerator SetNewScoreValue()
    {
        yield return new WaitForSeconds(1);
        tempScore = score.currentScore;
        backScore = score.currentScore - tempScore;
        haveBonus = false;
    }
}


