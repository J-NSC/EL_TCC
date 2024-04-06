using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManeger : MonoBehaviour
{
    void OnEnable()
    {
        PlayerCollider.triggeredQuestionArea += OnShowQuestion;
    }

    void OnDisable()
    {
        
    }


    void OnShowQuestion()
    {
      
    }
}
