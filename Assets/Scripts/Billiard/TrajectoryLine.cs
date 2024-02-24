using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    LineRenderer trajectory;
    [SerializeField] float range = 4f;

    void OnEnable()
    {
        Stick.trajectoryLine += OnRenderLine;
    }
    
    void OnDisable()
    {
        Stick.trajectoryLine -= OnRenderLine;
    }
    
    void Awake()
    {
        trajectory = GetComponent<LineRenderer>();
        // trajectory.enabled = false;
    }
    
    public void OnRenderLine(Vector3 endPosition)
    {
        trajectory.SetPosition(0 ,transform.position);
        trajectory.SetPosition(1, endPosition);
    }


}
