using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        PlayerCollider.startAnimationCauldron += OnStartAnimation;
    }
    
    void OnDisable()
    {
        PlayerCollider.startAnimationCauldron -= OnStartAnimation;
    }

    void OnStartAnimation()
    {
        anim.SetBool("Fire", true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
