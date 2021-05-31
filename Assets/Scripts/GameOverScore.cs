using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI highScore;

    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().currentScore.ToString();
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
