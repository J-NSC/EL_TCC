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

    void UpdateAnimation(string state, float speed, bool hasJump)
    {
        anim.Play(state);
        if (hasJump)
            anim.SetTrigger("Jump");
        
        anim.SetFloat("Speed", speed);
    }
}
