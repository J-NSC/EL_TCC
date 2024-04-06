using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControlle : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        Player.changedAnimation += UpdateAnimation;
    }

    void OnDisable()
    {
        Player.changedAnimation -= UpdateAnimation;
    }

    void UpdateAnimation(string state)
    {
        anim.Play(state);                
    }
}
