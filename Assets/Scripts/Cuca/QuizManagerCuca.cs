using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class QuizManagerCuca : MonoBehaviour
{
    public List<SOCucaGame> QnA;
    public GameObject[] options;
    public int currentQuestions;
    [SerializeField] QuizIndexSO quizIndex;

    public int IndexQuestion;

    public TMP_Text QuestionTxt;
    
    public delegate void CorrectedQuestionHandle();
    public static event CorrectedQuestionHandle correctQuestion;

    public delegate void GameOverHandler();
    public static event GameOverHandler gameOver;
    
    void OnEnable()
    {
        PlayerCollider.triggeredQuestionArea += Teste;
        // PlayerCollider.triggeredQuestionArea += (actived, name) =>
        // {
        //     Match match = Regex.Match(name, @"\d+");
        //     if (match.Success)
        //     {
        //         string indexNumber = match.Groups[0].Value;
        //         indexQuestion =  int.Parse(indexNumber);
        //     }
        // };
    }
    
    

    void Teste(bool actived, string name)
    {
        IndexQuestion = int.Parse(Regex.Match(name, @"\d+").Value);
        Debug.Log(IndexQuestion);
    }

    void OnDisable()
    {
        PlayerCollider.triggeredQuestionArea -= Teste;
    }

    void Start()
    {
        GenerateQuestion();
    }

    void Update()
    {
        GenerateQuestion();
    }

    public void Correct()
    {
        //questão certa
        QnA[IndexQuestion].correct = true;
        if (IndexQuestion == 0)
            quizIndex.activedPlatform_1 = true;
        GenerateQuestion();
        correctQuestion?.Invoke();
    }

    public void wrong()
    {
        //questão errada 
        // QnA.RemoveAt(currentQuestions);
        // GenerateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswersCuca>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[IndexQuestion].answers[i];
        
            if (QnA[IndexQuestion].correctAnswer == i)
            {
                options[i].GetComponent<AnswersCuca>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if (QnA.Count > 0 && IndexQuestion <= 1)
        {
            QuestionTxt.text = QnA[IndexQuestion].question;
            SetAnswers();  
        }
        else
        {
            gameOver?.Invoke();   
        }
    }
}
