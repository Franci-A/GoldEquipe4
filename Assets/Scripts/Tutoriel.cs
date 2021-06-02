using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    public static Tutoriel Instance { get { return instance; } }
    private static Tutoriel instance;

    public Animator animator;

    private void Start()
    {
        instance = this;
    }


    public void NextStep()
    {
        animator.SetTrigger("NextStep");
        FindObjectOfType<Bonus_Malus>().blockHand = false;
    }

    public void GetBonusTuto()
    {
        FindObjectOfType<Bonus_Malus>().getBonus(30);
        FindObjectOfType<Bonus_Malus>().blockHand = true; 
    }
}
