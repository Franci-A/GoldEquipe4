using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public bool isConnectedToGooglePlayServices;
    public bool gameOver = false;

    private void Awake()
    {
        instance = this;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        if(PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.SetInt("VIBRATION", 1);
            PlayerPrefs.SetInt("MUSIQUE", 1);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    break;
                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });

        gameOver = false;
    }

    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    break;
                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });
    }

    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);

        Time.timeScale = 0;
    }
}
