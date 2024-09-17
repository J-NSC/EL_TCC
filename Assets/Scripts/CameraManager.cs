using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera VCamera;
    void Update()
    {
         VCamera.Follow = FindObjectOfType<Player>().transform;
    }
}
