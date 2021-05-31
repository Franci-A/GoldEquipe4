using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public void playSound(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
        VibrationManager.Instance.VibrateThunder();
    }
}
