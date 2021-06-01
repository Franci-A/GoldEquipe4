using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuManager : MonoBehaviour
{

    public void Play(int index)
    {
        StartCoroutine(LoadLevel(index));
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

    IEnumerator LoadLevel(int index)
    {
        GetComponent<Animator>().SetTrigger("Play");

        yield return new WaitForSecondsRealtime(1.2f);

        if (PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            PlayerPrefs.SetInt("FirstLaunch", 1);
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }
}
