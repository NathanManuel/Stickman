using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    private PlayerGhost playerGhost;

    void Start()
    {
        playerGhost = GameObject.Find("Player1Ghost").GetComponent<PlayerGhost>();
    }

    private void OnMouseDown()
    {
        playerGhost.animationTrigger = "Kick";
    }

}
