using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentChanges : MonoBehaviour
{
    private Animator animator;
    public int turnsEmpty;
    private Tile parentTile;
    private bool hasTrees;

    public UnityEvent NextTurnEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        parentTile = GetComponent<Tile>();
        turnsEmpty = 0;
        NextTurnEvent.AddListener(UpdateTurn);
    }

    private void UpdateTurn()
    {
        if(parentTile.tileType == TileType.Ground && !hasTrees)
        {
            turnsEmpty++;
            float i = Random.Range(0f, 1f);
            switch (turnsEmpty)
            {
                case 5:
                    if(i < .05f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 6: 
                    if (i< .1f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 7: 
                    if (i< .15f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 8: 
                    if (i< .2f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 9:
                    if (i < .25f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 10:
                    turnsEmpty = 5;
                    break;

            }
        }else if(parentTile.tileType == TileType.Ground && hasTrees)
        {
            float i = Random.Range(0f, 1f);
            if ( i < .2f)
            {
                animator.SetTrigger("NextStage");
            }
        }
    }
}
