using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    private bool isOpen = false;
    private bool isMuted;
    private bool isVibration;
    [SerializeField] private LeftyFlip leftyFlip;

    private void Start()
    {
        optionMenuUI.SetActive(isOpen);
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
        isVibration = PlayerPrefs.GetInt("VIBRATION") == 1;
    }

    public void Option()
    {
        isOpen = !isOpen;
        optionMenuUI.SetActive(isOpen);
        PlayerPrefs.Save();
    }

    public void LoadMenu()
    {
        optionMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        optionMenuUI.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        AudioListener.pause = isMuted;
    }
    public void Mute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }
    public void Vibration()
    {
        isVibration = !isVibration;
        Debug.Log(isVibration);
        PlayerPrefs.SetInt("VIBRATION", isVibration ? 1 : 0);
    }

    public void FlipLeft()
    {
        PlayerPrefs.SetInt("LeftFlip", PlayerPrefs.GetInt("LeftFlip") == 0 ? 1 : 0);
        leftyFlip.FlipItems();
        PlayerPrefs.Save();
    }
}
