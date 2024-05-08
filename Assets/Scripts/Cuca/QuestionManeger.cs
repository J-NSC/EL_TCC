using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class QuestionManeger : MonoBehaviour
{

    [SerializeField] GameObject QuestionArea;
    [SerializeField] GameObject hidenPlataform;
    [SerializeField] List<SOCucaGame> cucaSo;
    [SerializeField] QuizIndexSO quizIndex;
    [SerializeField] GameObject SkyPlatforrm;


    void Awake()
    {
        QuestionArea = GameObject.FindGameObjectWithTag("QuestionArea");
    }

    void OnEnable()
    {
        PlayerCollider.triggeredQuestionArea += OnShowQuestion;
        QuizManagerCuca.correctQuestion += onShowedQuestionArea;
    }

    void OnDisable()
    {
        PlayerCollider.triggeredQuestionArea -= OnShowQuestion;
        QuizManagerCuca.correctQuestion -= onShowedQuestionArea;
    }

    void Start()
    {
        hidenPlataform.SetActive(false);
        QuestionArea.SetActive(false);
    }

    void onShowedQuestionArea()
    {
        QuestionArea.SetActive(false);
        hidenPlataform.SetActive(quizIndex.activedPlatform_1);
        if (quizIndex.activedPlatform_1)
        {
            SkyPlatforrm.SetActive(quizIndex.activedPlatform_2);
        }
    }
    void OnShowQuestion(bool actived, string name)
    {
        int index ;
        index = int.Parse(Regex.Match(name, @"\d+").Value);
        if (!cucaSo[index].correct)
        {
            QuestionArea.SetActive(actived);
        }

    }
}
