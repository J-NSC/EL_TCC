using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InfoPlate : MonoBehaviour
{
    [SerializeField] SignInfoSO signInfoSo;
    [SerializeField] GameObject houseGame;
    [SerializeField] GameObject canvasObj;

    void Awake()
    {
        canvasObj = houseGame.transform.GetChild(2).gameObject;
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

    void OnShowedInfo(bool showed)
    {
        canvasObj.SetActive(showed);
    }
}
