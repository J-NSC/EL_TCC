using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;


public class QuizManage : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public TMP_Text QuestionTxt;
    
    public delegate void ScoreSendHandler(string msg);
    public static event ScoreSendHandler scoreSend;

    public delegate void GameOverHandler();

    public static event GameOverHandler gameOver;

    [SerializeField] SOCucaGame cucaSO;

    int totalQuestions = 0;
    public int score = 0;

    void Start()
    {
        // QnA = new List<QuestionAndAnswers>(Resources.LoadAll<QuestionAndAnswers>("ScriptObject/Quiz"));
        totalQuestions = QnA.Count;
        GenerateQuestion();      
        scoreSend?.Invoke($"Acertos: {score.ToString()}/{totalQuestions}");
    }

    void Update()
    {
        Debug.Log(score+ "/ " +totalQuestions);
    }

    public void Correct()
    {
        //questão certa
        score++;
        QnA.RemoveAt(currentQuestions);
        GenerateQuestion();
        scoreSend?.Invoke($"Acertos: {score.ToString()}/{totalQuestions}");
    }

    public void wrong()
    {
        //questão errada 
        QnA.RemoveAt(currentQuestions);
        GenerateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestions].answers[i];
        
            if (QnA[currentQuestions].correctAnswer == i)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestions = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestions].question;
            SetAnswers();  
        }
        else
        {
            cucaSO.correct = true;  
            StartCoroutine(CallGameOver());
        }
    }
    
    IEnumerator CallGameOver()
    {
        yield return new WaitForSeconds(.2f);
        gameOver?.Invoke();  
    }
}
