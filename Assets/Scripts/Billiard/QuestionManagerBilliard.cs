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

    public delegate void ValidatedQuestionHandle(bool msg);
    public static event ValidatedQuestionHandle validededQuestion;

    public delegate void SendScoreBilliardHandler(string msg);
    public static event SendScoreBilliardHandler SendScoreBilliard;

    public delegate void GameOverScreenHandler();
    public static event GameOverScreenHandler gameOver;

    public delegate void EnabledDoubleJumpHandler(string name, bool isEnabled);
    public static event  EnabledDoubleJumpHandler enabelDoubleJump;
    
    int currentQuestion = 0;
    int correctQuestion = 0;
    int CountQuestion = 0;

    void Start()
    {
        setRandonQuestion();
        CountQuestion = QnB.Count;
        SendScoreBilliard?.Invoke($"Acertos:{correctQuestion}/{CountQuestion}");
    }


    public void OnQuestChecked(string msg)
    {
        if (QnB[currentQuestion].correctAnswer.ToLower() == msg.ToLower())
        {
            correctQuestion++;
            StartCoroutine(ValidedQuestionTime(true));
        }else
            StartCoroutine(ValidedQuestionTime(false));
        
        SendScoreBilliard?.Invoke($"Questoes Corretas:{correctQuestion}/{CountQuestion}");

        QnB.RemoveAt(currentQuestion);
        setRandonQuestion();
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
            if (correctQuestion >= (CountQuestion / 2))
            {
                enabelDoubleJump?.Invoke( "Pulo Duplo",true);
            }

            StartCoroutine(CallGameOver());

        }
    }

    IEnumerator CallGameOver()
    {
        yield return new WaitForSeconds(.2f);
        gameOver?.Invoke();  
    }

    IEnumerator ValidedQuestionTime(bool msg)
    {
        yield return new WaitForSeconds(.1f);
        validededQuestion?.Invoke( msg);
    }
}
