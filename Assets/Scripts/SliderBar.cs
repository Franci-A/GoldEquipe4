using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider ScoreBar;

    void Update()
    {
        ScoreBar.value = GetComponent<Score>().currentScore;

        if (ScoreBar.value >= 100){
            ScoreBar.value = 0;
        }
    }
}
