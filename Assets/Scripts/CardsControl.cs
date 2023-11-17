using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsControl : MonoBehaviour
{

    [SerializeField] private GameObject[] cards;
    public Vector3 showCardsScale = new Vector3(0.1f, 0.9f, 0.6f);
    public Vector3 hideCardsScale = new Vector3(0.1f, 0.4f, 0.3f);
    public Vector3 moveCards = new Vector3(0f, 0f, 1f);
    public float spacing = 0.4f;
    private bool showCards;


    private void OnMouseDown()
    {
        float xOffset = 0.3f;


        // Rotate the cube to (10, 0, 0) when clicked
        if (showCards)
        {
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
        else
        {
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
        showCards = !showCards;
    }
}
