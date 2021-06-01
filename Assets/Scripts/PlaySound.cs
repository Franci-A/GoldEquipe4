using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void playSound(string name)
    {
        VibrationManager.Instance.VibrateThunder();
        FindObjectOfType<AudioManager>().Play(name);
    }
}
