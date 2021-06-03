using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] GameObject newBest;
    int currentScore;
    int updateScoreNum;
    bool canUpdateScore;
    int previousHighScore;
    bool newHighScore =false;

    // Start is called before the first frame update
    void Start()
    {
        canUpdateScore = false;
        currentScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().currentScore;
        previousHighScore = PlayerPrefs.GetInt("HighScore");
        if(Score.Instance.CheckHighScore())
        {
            newHighScore = true;
        }
    }
    private void Update()
    {
        if(canUpdateScore && updateScoreNum < currentScore)
        {
            updateScoreNum+= 11;
            scoreText.text = updateScoreNum.ToString();
        }
        else if(canUpdateScore && updateScoreNum >= currentScore)
        {
            updateScoreNum = currentScore;
            scoreText.text = updateScoreNum.ToString();
        }

        if(canUpdateScore && newHighScore)
        {
            if (previousHighScore < PlayerPrefs.GetInt("HighScore"))
            {
                previousHighScore += 11;
                highScore.text = previousHighScore.ToString();
            }
            else if (previousHighScore >= PlayerPrefs.GetInt("HighScore"))
            {
                previousHighScore = PlayerPrefs.GetInt("HighScore"); 
                highScore.text = previousHighScore.ToString();
                newBest.SetActive(true);
            }
        }else if (canUpdateScore)
        {
            highScore.text = previousHighScore.ToString();
        }
    }

    public void CanUpdateScore()
    {
        canUpdateScore = true;
    }

}
