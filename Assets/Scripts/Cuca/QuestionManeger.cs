using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManeger : MonoBehaviour
{

    [SerializeField] GameObject QuestionArea;

    void Awake()
    {
        QuestionArea = GameObject.FindGameObjectWithTag("QuestionArea");
        QuestionArea.SetActive(false);
    }

    void OnEnable()
    {
        PlayerCollider.triggeredQuestionArea += OnShowQuestion;
    }

    void OnDisable()
    {
        PlayerCollider.triggeredQuestionArea -= OnShowQuestion;
    }


    void OnShowQuestion(bool actived)
    {
        QuestionArea.SetActive(actived);      
    }
}
