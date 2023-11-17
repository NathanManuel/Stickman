using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;

    private GameManager gm;
    [SerializeField] private Vector3 mouseHoverOverCard;
    [SerializeField] public Vector3 intialCardPosition;


    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        mouseHoverOverCard = gm.cardSlots[handIndex].transform.position + (Vector3.up * 0.05f);
        intialCardPosition = gm.cardSlots[handIndex].transform.position;

    }

    private void OnMouseDown()
    {
        if (gm.selectedCard != null)
        {
            gm.selectedCard.transform.position = gm.selectedCard.intialCardPosition;
        }
        gm.selectedCard = this;
    }

    private void OnMouseOver()
    {
        transform.position = mouseHoverOverCard;
    }
    private void OnMouseExit()
    {
        if (gm.selectedCard != this)
        {
            transform.position = intialCardPosition;
        }
    }
    public void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }

}
