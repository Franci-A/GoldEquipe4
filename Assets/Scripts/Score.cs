using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get { return instance; } }
    private static Score instance;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    public int currentScore = 0;


    void Start()
    {
        instance = this;
        scoreDisplay.text = "" + currentScore;
    }

    public void AddScore(int pointWon)
    {
        currentScore += pointWon;
        if (currentScore < 0)
            currentScore = 0;
        scoreDisplay.text = "" + currentScore;

        if(currentScore >= 250)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQBg"); // score achievement
        }
        else if(currentScore >= 500)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQBw"); // score achievement
        }
        else if(currentScore >= 750)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQCA"); // score achievement
        }else if(currentScore >= 1000)
        {
            AchievementManager.Instance.UnlockAchievement("CgkIp7jc_LgZEAIQEQ");
        }
    }

    public void CheckHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);

            if (GameManager.Instance.isConnectedToGooglePlayServices)
            {
                Debug.Log("Reporting score...");
                Social.ReportScore(PlayerPrefs.GetInt("HighScore"), GPGSIds.leaderboard_high_score, (success) =>
                {
                    if (!success)
                        Debug.Log("Unable to post highScore");
                });
            }
            else
            {
                Debug.Log("Not signed in, unable to report score");
            }
            PlayerPrefs.Save();
        }
    }
}

//PlayerPrefs.GetInt("Player Score");