using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGameManeger : MonoBehaviour
{
    [SerializeField] List<GameObject> housesGame;

    void OnEnable()
    {
        PlayerCollider.sendNameHouse += OnChangedHouse;
    }

    void OnDisable()
    {
        PlayerCollider.sendNameHouse -= OnChangedHouse;
    }

    void Start()
    {
        foreach (var house in housesGame)
        {
            if (house.name != "HouseBilliards")
            {
                Animator houseAnimator = house.GetComponent<Animator>();
                houseAnimator.enabled = false;
            }
        }
    }

    void Update()
    {
    
    }

    void OnChangedHouse(string name, bool actived)
    {
        foreach (var house in housesGame)
        {
            if (house.name == name)
            {
                Animator houseAnimator = house.GetComponent<Animator>();
                houseAnimator.enabled = actived;
            }
        }
    }

 
}
