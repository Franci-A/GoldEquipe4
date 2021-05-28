using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    private bool isOpen = false;
    private bool isSound;
    private bool isVibration;
    [SerializeField] private LeftyFlip leftyFlip;

    public GameObject vibrationOn;
    public GameObject vibrationOff;

    public GameObject soundOn;
    public GameObject soundOff;

    public GameObject musicOn;
    public GameObject musicOff;

    private void Start()
    {
        optionMenuUI.SetActive(isOpen);
        isSound = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isSound;
        if(isSound == false)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }

        isVibration = PlayerPrefs.GetInt("VIBRATION") == 1;
        if (isVibration == false)
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
        }
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
        AudioListener.pause = isSound;
    }



    public void SoundOn()
    {
        isSound = false;
        AudioListener.pause = isSound;
        PlayerPrefs.SetInt("MUTED", isSound ? 1 : 0);
        soundOn.SetActive(false);
        soundOff.SetActive(true);
        PlayerPrefs.Save();
    }
    public void SoundOff()
    {
        isSound = true;
        AudioListener.pause = isSound;
        PlayerPrefs.SetInt("MUTED", isSound ? 1 : 0);
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        PlayerPrefs.Save();
    }



    public void VibrationOn()
    {
        isVibration = false;
        Debug.Log(isVibration);
        PlayerPrefs.SetInt("VIBRATION", isVibration ? 1 : 0);
        vibrationOn.SetActive(false);
        vibrationOff.SetActive(true);
        PlayerPrefs.Save();

    }
    public void VibrationOff()
    {
        isVibration = true;
        Debug.Log(isVibration);
        PlayerPrefs.SetInt("VIBRATION", isVibration ? 1 : 0);
        vibrationOff.SetActive(false);
        vibrationOn.SetActive(true);
        PlayerPrefs.Save();
    }




    public void FlipLeft()
    {
        PlayerPrefs.SetInt("LeftFlip", PlayerPrefs.GetInt("LeftFlip") == 0 ? 1 : 0);
        leftyFlip.FlipItems();
        PlayerPrefs.Save();
    }
}
