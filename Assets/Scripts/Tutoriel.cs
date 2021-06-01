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
    }
}
