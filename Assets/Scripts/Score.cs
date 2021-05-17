using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    int currentScore = 0;
    void Start()
    {
        PlayerPrefs.SetInt("Player Score", currentScore);
        scoreDisplay.text = "Score: " + currentScore;
    }

    public void AddScore(int pointWon)
    {
        currentScore += pointWon;
        scoreDisplay.text = "Score: " + currentScore;
    }
}

//PlayerPrefs.GetInt("Player Score");