using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;

    private GameManager gm;
    private Vector3 hover;
    public Vector3 intialPos;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        hover = gm.cardSlots[handIndex].transform.position + (Vector3.up * 0.05f);
        intialPos = gm.cardSlots[handIndex].transform.position;

    }

    private void OnMouseDown()
    {
        if (gm.selectedCard != null)
        {
            gm.selectedCard.transform.position = gm.selectedCard.intialPos;
        }
        gm.selectedCard = this;
    }

    private void OnMouseOver()
    {
        transform.position = hover;
    }
    private void OnMouseExit()
    {
        if (gm.selectedCard != this)
        {
            transform.position = intialPos;
        }
    }
    public void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }

}
