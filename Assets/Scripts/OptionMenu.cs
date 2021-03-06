using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    private bool isOpen = false;
    private bool isSound = true;
    private bool isMusic = true;
    private bool isVibration = true;
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
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        vibrationOn.SetActive(true);
        vibrationOff.SetActive(false);

        isSound = PlayerPrefs.GetInt("MUTED") == 1;
        if (isSound == false)
        {
            SoundOff();
        }

        isMusic = PlayerPrefs.GetInt("MUSIQUE") == 1;
        if(isMusic == false)
        {
            MusicOff();
        }

        isVibration = PlayerPrefs.GetInt("VIBRATION") == 1;
        if (isVibration == false)
        {
            VibrationOff();
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
    }



    public void SoundOn()
    {
        isSound = true;
        FindObjectOfType<AudioManager>().Sfx("Music", true);
        PlayerPrefs.SetInt("MUTED", 1);
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        PlayerPrefs.Save();
    }
    public void SoundOff()
    {
        isSound = false;
        FindObjectOfType<AudioManager>().Sfx("Music", false);
        PlayerPrefs.SetInt("MUTED", 0);
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        PlayerPrefs.Save();
    }

    public void MusicOn()
    {
        isMusic = true;
        FindObjectOfType<AudioManager>().Music("Music", "UnPause");
        PlayerPrefs.SetInt("MUSIQUE", 1);
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        PlayerPrefs.Save();
    }    
    public void MusicOff()
    {
        isMusic = false;
        FindObjectOfType<AudioManager>().Music("Music", "Pause");
        PlayerPrefs.SetInt("MUSIQUE", 0);
        musicOff.SetActive(true);
        musicOn.SetActive(false);
        PlayerPrefs.Save();
    }

    public void VibrationOn()
    {
        isVibration = false;
        Debug.Log(isVibration);
        PlayerPrefs.SetInt("VIBRATION", 0);
        vibrationOn.SetActive(false);
        vibrationOff.SetActive(true);
        PlayerPrefs.Save();

    }
    public void VibrationOff()
    {
        isVibration = true;
        PlayerPrefs.SetInt("VIBRATION", 1);
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
