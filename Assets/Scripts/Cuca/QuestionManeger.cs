using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class QuestionManeger : MonoBehaviour
{

    [SerializeField] GameObject QuestionArea;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject hidenPlataform;
    [SerializeField] List<SOCucaGame> cucaSo;
    [SerializeField] QuizIndexSO quizIndex;
    [SerializeField] GameObject SkyPlatforrm;


    void Awake()
    {
        QuestionArea = GameObject.FindGameObjectWithTag("QuestionArea");
        buttons = GameObject.FindGameObjectWithTag("QuestionButtons");
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
        SkyPlatforrm.SetActive(false);
        QuestionArea.SetActive(false);
        buttons.SetActive(false);
    }

    void Update()
    {
        if (quizIndex.activedPlatform_2)
        {
            SkyPlatforrm.SetActive(quizIndex.activedPlatform_2);
        }
    }

    void onShowedQuestionArea()
    {
        QuestionArea.SetActive(false);
        buttons.SetActive(false);
        hidenPlataform.SetActive(quizIndex.activedPlatform_1);
     
    }
    void OnShowQuestion(bool actived, string name)
    {
        int index ;
        index = int.Parse(Regex.Match(name, @"\d+").Value);
        if (!cucaSo[index].correct)
        {
            QuestionArea.SetActive(actived);
            buttons.SetActive(actived);
        }

    }
}
