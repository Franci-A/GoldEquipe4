using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;

    private void Start()
    {
        optionMenuUI.SetActive(false);
    }

    public void Resume()
    {
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Option()
    {
        optionMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
