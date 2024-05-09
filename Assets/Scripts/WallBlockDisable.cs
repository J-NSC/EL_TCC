using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockDisable : MonoBehaviour
{
    [SerializeField] GameObject wallBlock;
    [SerializeField] SOCucaGame cucaSO;
   


    void Start()
    {
        wallBlock.SetActive(true);
    }

    void Update()
    {
        if (cucaSO.correct)
        {
            OnDisableWall();
        }
    }

    void OnDisableWall()
    {
        wallBlock.SetActive(false);
    }
}
