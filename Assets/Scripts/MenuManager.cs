using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuManager : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void Leaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    
    public void Achievementboard()
    {
        Social.ShowAchievementsUI();
    }
    public void BackCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
