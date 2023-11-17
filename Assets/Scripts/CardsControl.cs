using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControl : MonoBehaviour
{

    [SerializeField] private GameObject[] cards;
    [SerializeField] private Vector3 showCardsScale = new Vector3(0.1f, 0.9f, 0.6f);
    [SerializeField] private Vector3 hideCardsScale = new Vector3(0.1f, 0.4f, 0.3f);
    [SerializeField] private Vector3 moveCards = new Vector3(0f, 0f, 1f);
    [SerializeField] private float spacing = 0.4f;
    [SerializeField] private bool showCards;


   private void OnMouseDown()
{
    if (showCards)
    {
        ShowCards();
    }
    else
    {
        HideCards();
    }

    showCards = !showCards;
}

private void ShowCards()
{
    float xOffset = 0.3f;
    cards[0].transform.rotation = Quaternion.Euler(4, 0, 0);
    cards[2].transform.rotation = Quaternion.Euler(-4, 0, 0);

    foreach (GameObject obj in cards)
    {
        obj.transform.localScale = showCardsScale;
        Vector3 newPosition = transform.position + new Vector3(0, 0, -xOffset);
        obj.transform.position = newPosition;
        xOffset -= spacing;
    }
}

private void HideCards()
{
    float xOffset = 0.3f;

    foreach (GameObject obj in cards)
    {
        obj.transform.localScale = hideCardsScale;
        Vector3 newPosition = transform.position + new Vector3(0, -0.3f, xOffset);
        obj.transform.position = newPosition;
        xOffset += spacing;
    }

    cards[0].transform.rotation = Quaternion.Euler(0, 0, 0);
    cards[2].transform.rotation = Quaternion.Euler(0, 0, 0);
}

}
