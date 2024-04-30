using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InfoPlate : MonoBehaviour
{
    SignInfoSO signInfoSo;
    [SerializeField] GameObject houseGame;
    [SerializeField] GameObject canvasObj;

    void Awake()
    {
        houseGame = GameObject.FindWithTag("MiniGameHouse");
        canvasObj = houseGame.transform.GetChild(1).gameObject;
    }

    void OnEnable()
    {
        PlayerCollider.plateShowed += OnShowedInfo;
    }

    void OnDisable()
    {
        PlayerCollider.plateShowed -= OnShowedInfo;
    }

    void Start()
    {
        canvasObj.SetActive(false);
    }

    
    void Update()
    {
        
    }

    void OnShowedInfo(bool showed)
    {
           canvasObj.SetActive(showed);
    }
}
