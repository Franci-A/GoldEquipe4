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
    public bool lightningStrike;
    public int lightningStrikeLvl;
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
        if (lightningStrike)
        {
            lightningStrikeLvl++;
            StartCoroutine(LightningMark());
        }
        else if(!lightningStrike)
        {
            parentTile.lightningMark.sprite = parentTile.sprites.GetSprite("Thunder", "Level0");
        }
        else if(parentTile.tileType == TileType.Ground && !hasTrees)
        {
            turnsEmpty++;
            float i = Random.Range(0f, 1f);
            if (turnsEmpty > 5 && i < .05f)
            {
                animator.SetTrigger("NextStage");
                hasTrees = true;
            }
        }
        else if(parentTile.tileType == TileType.Ground && hasTrees)
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
