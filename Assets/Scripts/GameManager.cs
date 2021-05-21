using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return Instance; } }
    private static GameManager instance;

    public bool isConnectedToGooglePlayServices;

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        SignInToGooglePlayServices();
    }

    public void SignInToGooglePlayServices()
    {
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
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        Score.Instance.CheckHighScore();
    }
}
