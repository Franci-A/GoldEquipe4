using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    public int currentScore = 0;
    void Start()
    {
        PlayerPrefs.SetInt("Player Score", currentScore);
        scoreDisplay.text = "" + currentScore;
    }

    public void AddScore(int pointWon)
    {
        currentScore += pointWon;
        if (currentScore < 0)
            currentScore = 0;
        scoreDisplay.text = "" + currentScore;
    }
}

//PlayerPrefs.GetInt("Player Score");