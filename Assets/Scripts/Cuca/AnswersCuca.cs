using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersCuca : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManagerCuca quizManage;

   

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
