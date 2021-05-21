using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    }
    public void BackCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
