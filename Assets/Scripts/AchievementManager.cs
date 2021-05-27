using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get { return _instance; } }
    private static AchievementManager _instance;

    private void Start()
    {
        _instance = this;
    }

    public  void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, (success) => { });
    }

    public static void IncrementAchievement(string id, int stepToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepToIncrement, success => { });
    }

}
