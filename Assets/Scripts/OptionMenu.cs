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


        if (PlayerPrefs.GetInt("SFX") == 0)
        {
            SoundOff();
        }
        else
        {
            SoundOn();
        }

        if(PlayerPrefs.GetInt("MUSIQUE") == 0)
        {
            Debug.Log(PlayerPrefs.GetInt("MUSIQUE"));
            MusicOff();
        }
        else
        {
            MusicOn();
        }

        
        if (PlayerPrefs.GetInt("VIBRATION") == 0)
        {
            VibrationOff();
        }else
        {
            VibrationOn();
        }
    }


    public void LoadMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    public void Retry()
    {
        StartCoroutine(LoadLevel());
    }

    public void SoundOn()
    {
        isSound = true;
        AudioManager.instance.Sfx("Music", true);
        PlayerPrefs.SetInt("SFX", 1);
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        PlayerPrefs.Save();
    }
    public void SoundOff()
    {
        isSound = false;
        AudioManager.instance.Sfx("Music", false);
        PlayerPrefs.SetInt("SFX", 0);
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        PlayerPrefs.Save();
    }

    public void MusicOn()
    {
        isMusic = true;
        AudioManager.instance.Music("Music", "UnPause");
        PlayerPrefs.SetInt("MUSIQUE", 1);
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        PlayerPrefs.Save();
    }    
    public void MusicOff()
    {
        isMusic = false;
        AudioManager.instance.Music("Music", "Pause");
        PlayerPrefs.SetInt("MUSIQUE", 0);
        musicOff.SetActive(true);
        musicOn.SetActive(false);
        PlayerPrefs.Save();
    }

    public void VibrationOn()
    {
        isVibration = true;
        VibrationManager.Instance.vibration = true;
        PlayerPrefs.SetInt("VIBRATION", 1);
        vibrationOn.SetActive(true);
        vibrationOff.SetActive(false);
        PlayerPrefs.Save();

    }
    public void VibrationOff()
    {
        isVibration = false;
        VibrationManager.Instance.vibration = false;
        PlayerPrefs.SetInt("VIBRATION", 0);
        vibrationOn.SetActive(false);
        vibrationOff.SetActive(true);
        PlayerPrefs.Save();
    }

    public void FlipLeft()
    {
        PlayerPrefs.SetInt("LeftFlip", PlayerPrefs.GetInt("LeftFlip") == 0 ? 1 : 0);
        leftyFlip.FlipItems();
        PlayerPrefs.Save();
    }

    IEnumerator LoadLevel()
    {
        GetComponent<Animator>().SetTrigger("Play");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }
    IEnumerator LoadMainMenu()
    {
        GetComponent<Animator>().SetTrigger("Play");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }
}
