using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void OnEnable()
    {
        PlayerCollider.changedLayer += OnChanchedLayer;
    }

    void OnDisable()
    {
        PlayerCollider.changedLayer -= OnChanchedLayer;
    }

    void Start()
    {
    }

    void OnChanchedLayer(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
}
