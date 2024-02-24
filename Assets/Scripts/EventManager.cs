using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    Ball ball;
    GameSetup gameSetup;
    Stick stick;

    QuestionManagerBilliard questionManagerBilliard;
    HUDManager hudManager;

    void Awake()
    {
        gameSetup = FindObjectOfType<GameSetup>();
        stick = FindObjectOfType<Stick>();

        questionManagerBilliard = FindObjectOfType<QuestionManagerBilliard>();
        hudManager = FindObjectOfType<HUDManager>();
    

    }

    void OnEnable()
    {
        questionManagerBilliard.questionGenerated += hudManager.OnQuestionGeneratedBilliard;
    }

    void OnDisable()
    {
        questionManagerBilliard.questionGenerated -= hudManager.OnQuestionGeneratedBilliard;
    }

    void Update()
    {
        ball = FindObjectOfType<Ball>();
        if (ball.isDesroyed)
        {
            Invoke("InvikeDestroyBall", .5f);
            Invoke("InvokeResetStick", .5f);
            ball.isDesroyed = false;
        }
    }

    void InvikeDestroyBall()
    {
        gameSetup.OnBallDestroye();
    }

    void InvokeResetStick()
    {
        stick.OnResetStick();
    }
}
