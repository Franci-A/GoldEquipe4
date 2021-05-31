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


    public void PlayAnim()
    {
        GetComponent<Animator>().SetTrigger("Play");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void Leaderboard()
    {
        if (GameManager.Instance.isConnectedToGooglePlayServices)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            GameManager.Instance.SignInToGooglePlayServices();
            Social.ShowLeaderboardUI();
        }
    }
    
    public void Achievementboard()
    {
        if (GameManager.Instance.isConnectedToGooglePlayServices)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            Social.ShowAchievementsUI();
            GameManager.Instance.SignInToGooglePlayServices();
        }
    }
    public void BackCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/Summum_TheGame");
    }
}
