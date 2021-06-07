using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.U2D.Animation;

public class EnvironmentChanges : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator animalAnimator;
    public int turnsEmpty;
    private Tile parentTile;
    private bool hasTrees;
    private int randomTrees;
    public bool lightningStrike;
    public int lightningStrikeLvl;
    [SerializeField] private SpriteRenderer sprite;
    private int treeLevel;

    public UnityEvent NextTurnEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        parentTile = GetComponent<Tile>();
        turnsEmpty = 0;
        NextTurnEvent.AddListener(UpdateTurn);
        randomTrees = Random.Range(0, 8);
        animator.SetFloat("Vegetation", randomTrees);
        animalAnimator.SetFloat("Animal", randomTrees);
        EmptyTile();
    }

    private void UpdateTurn()
    {
        if (lightningStrike)
        {
            lightningStrikeLvl++;
            StartCoroutine(LightningMark());
        }
        else if(parentTile.tileType == TileType.Ground && !hasTrees)
        {
            turnsEmpty++;
            float i = Random.Range(0f, 1f);
            if (turnsEmpty > 5 && i < .05f)
            {
                animator.SetTrigger("NextStage");
                treeLevel = 1;
                hasTrees = true;
            }
        }
        else if(parentTile.tileType == TileType.Ground && hasTrees)
        {
            float i = Random.Range(0f, 1f);
            if ( i < .1f)
            {
                animator.SetTrigger("NextStage");
                treeLevel++;
            }
        }
        else if (!lightningStrike)
        {
            parentTile.lightningMark.sprite = parentTile.sprites.GetSprite("Thunder", "Level0");
        }

        if(treeLevel > 3)
        {
            parentTile.animal.SetActive(true);
            animalAnimator.SetFloat("Animal", randomTrees);
        }
        else
        {
            parentTile.animal.SetActive(false);
        }
    }

    public void EmptyTile()
    {
        treeLevel = 0; 
        parentTile.animal.SetActive(false);
        turnsEmpty = 0;
        int i = Random.Range(0, 6);
        GetComponent<Animator>().SetFloat("GrassLevel", i);
    }

    IEnumerator LightningMark()
    {
        yield return new WaitForSeconds(1f);

        parentTile.lightningMark.sprite = parentTile.sprites.GetSprite("Thunder", "Level" + lightningStrikeLvl.ToString());
        if (lightningStrikeLvl == 5)
        {
            lightningStrike = false;
            lightningStrikeLvl = 0;
        }
    }
}
