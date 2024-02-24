using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManage quizManage;

   

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("resposta correta");
            quizManage.Correct();
        }
        else
        {
            Debug.Log("resposta errada");
            quizManage.wrong();
        }
    }
}
