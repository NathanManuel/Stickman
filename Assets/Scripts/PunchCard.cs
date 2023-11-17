using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCard : MonoBehaviour
{
    private PlayerGhost playerGhost;

    void Start()
    {
        playerGhost = GameObject.Find("Player1Ghost").GetComponent<PlayerGhost>();
    }

    private void OnMouseDown()
    {
        playerGhost.animationTrigger = "Punch";
    }


}


