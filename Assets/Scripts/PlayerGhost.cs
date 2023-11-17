using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    private Animator animator;


    public string chosen;
    public string animationTrigger;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



}
