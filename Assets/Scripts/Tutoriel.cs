using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    public static Tutoriel Instance { get { return instance; } }
    private static Tutoriel instance;

    public Animator animator;
    [SerializeField] private int currentStep = 1;

    private void Start()
    {
        instance = this;
    }


    public void NextStep()
    {
        animator.SetTrigger("NextStep");
        currentStep++;
        switch (currentStep)
        {
            case 2:
                FindObjectOfType<Bonus_Malus>().blockHand = false;
                break;
            case 3:
                FindObjectOfType<Bonus_Malus>().getBonus(30);
                break;
        }
    }

}
