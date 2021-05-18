using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider ScoreBar;
    public int backScore;
    public int random;
    public int i = 1;
    void Update()
    {
        backScore =(int)(GetComponent<Score>().currentScore % ScoreBar.maxValue);
        ScoreBar.value = backScore;

        if (GetComponent<Score>().currentScore >= (i * ScoreBar.maxValue))
        {
            random = Random.Range(1, 5);
            Debug.Log(random);
            ScoreBar.value = 0;
            i++;
            
        }
    }

}
