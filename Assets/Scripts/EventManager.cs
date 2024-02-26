using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    Ball ball;
    GameSetup gameSetup;
    Stick stick;
    [SerializeField] GameObject objectStick; 

    QuestionManagerBilliard questionManagerBilliard;
    HUDManager hudManager;

    [SerializeField] float time;

    void Awake()
    {
        gameSetup = FindObjectOfType<GameSetup>();
        stick = FindObjectOfType<Stick>();

        questionManagerBilliard = FindObjectOfType<QuestionManagerBilliard>();
        hudManager = FindObjectOfType<HUDManager>();
    

    }

    void OnEnable()
    {
        Ball.enebledBall += stick.OnEnabledStick;
        questionManagerBilliard.questionGenerated += hudManager.OnQuestionGeneratedBilliard;
        HUDManager.activetedStick += stick.OnEnabledStick;
    }


    void Update()
    {
        ball = FindObjectOfType<Ball>();
        if (ball.isDesroyed)
        {
            Invoke("InvokeDestroyBall", .5f);
            Invoke("InvokeResetStick", .5f);
            Ball.enebledBall += stick.OnEnabledStick;
            ball.isDesroyed = false;
        }

    }

    void InvokeDestroyBall()
    {
        gameSetup.OnBallDestroye();
        Ball.enebledBall -= stick.OnEnabledStick;
    }

    void InvokeResetStick()
    {
        stick.OnResetStick();
    }
}
