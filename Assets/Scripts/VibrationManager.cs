using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get { return instance; } }
    private static VibrationManager instance;
    public bool vibration;
    public bool isFiled;

    void Awake()
    {
        instance = this;
    }
    public void VibrateMerge()
    {
        if (vibration)
        {
            Debug.Log("Vrrr!");
            Handheld.Vibrate();
        }
    }

    public void VibrateThunder()
    {
        if (vibration && isFiled)
        {
            Debug.Log("Vrrr!");
            Handheld.Vibrate();
        }
    }
}
