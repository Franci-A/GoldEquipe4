using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimatorLink : MonoBehaviour
{
    public Dragon dragonScript;


    public void DragonBall()
    {
        foreach (Tile item in dragonScript.targetedTiles)
        {
            item.targeted.SetTrigger("FireBall");
        }
        
    }
    public void Target()
    {
        foreach (Tile item in dragonScript.targetedTiles)
        {
            item.targeted.SetTrigger("Target");
        }
        
    }

    public void Attack()
    {
        dragonScript.DragonAttack();
    }

    public void playSound(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

}
