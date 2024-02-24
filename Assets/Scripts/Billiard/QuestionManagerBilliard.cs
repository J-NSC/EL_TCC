using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionManagerBilliard : MonoBehaviour
{
    public List<QuestionsBilliard> QnB;

    public delegate void QuestionGeneratedHandle(string msg);

    public event QuestionGeneratedHandle questionGenerated;
    
    int currentQuestion = 0;

    void Start()
    {
        setRandonQuestion();
    }


    public void OnQuestChecked(string msg)
    {
        // if (QnB[currentQuestion].correctAnswer.ToLower() == msg.ToLower())
        // {
        //     Debug.Log("acertou misseravel");
        //     QnB.RemoveAt(currentQuestion);
        //     setRandonQuestion();
        // }
        // else
        // {
        //     Debug.Log("errou");
        //     QnB.RemoveAt(currentQuestion);
        //     setRandonQuestion();
        // }
    }
    

    public void setRandonQuestion()
    {
        if (QnB.Count > 0)
        {
            currentQuestion = Random.Range(0, QnB.Count);
            
            if (questionGenerated != null)
            {
                questionGenerated(QnB[currentQuestion].question);
            }
        }
        else
        {
            // game over    
        }
    }
}
