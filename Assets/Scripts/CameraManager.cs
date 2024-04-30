using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera VCamera;


    void Awake()
    {
        VCamera.Follow = FindObjectOfType<Player>().transform;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
