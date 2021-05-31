using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI highscoreText;
    void Start()
    {
        highscoreText = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("HighScore") > 0)
        {
            highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }
}
