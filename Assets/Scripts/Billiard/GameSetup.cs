using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSetup : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;

    Ball ball;
    
    void Awake()
    {
        OnBallDestroye();
    }

    public void OnBallDestroye()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }


 
}
