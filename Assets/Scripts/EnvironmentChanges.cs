using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.U2D.Animation;

public class EnvironmentChanges : MonoBehaviour
{
    private Animator animator;
    public int turnsEmpty;
    private Tile parentTile;
    private bool hasTrees;
    private int randomTrees;
    [SerializeField] private SpriteRenderer sprite;

    public UnityEvent NextTurnEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        parentTile = GetComponent<Tile>();
        turnsEmpty = 0;
        NextTurnEvent.AddListener(UpdateTurn);
        randomTrees = Random.Range(0, 8);
        animator.SetFloat("Vegetation", randomTrees);
        EmptyTile();
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
                    if(i < .025f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 6: 
                    if (i< .05f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 7: 
                    if (i< .075f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 8: 
                    if (i< .1f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 9:
                    if (i < .125f)
                    {
                        animator.SetTrigger("NextStage");
                        hasTrees = true;
                    }
                    break;
                case 10:
                    turnsEmpty = 0;
                    break;

            }
        }else if(parentTile.tileType == TileType.Ground && hasTrees)
        {
            float i = Random.Range(0f, 1f);
            if ( i < .1f)
            {
                animator.SetTrigger("NextStage");
            }
        }
    }

    public void EmptyTile()
    {
        turnsEmpty = 0;
        int i = Random.Range(0, 6);
        sprite.sprite = GetComponent<SpriteLibrary>().GetSprite("Grass", i.ToString());
    }
}
