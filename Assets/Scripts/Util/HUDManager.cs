using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text QuestionText;

    [Header("FeedBackSrcreen")] 
    [SerializeField] Sprite[] emoji;
    [SerializeField]GameObject screen; 
    [SerializeField] Image emojiFeedBack;
    [SerializeField] TMP_Text textFeedBack;
    [SerializeField] string[] messageFeedBack;
    
    public delegate void ActivetedStickHandle(bool activeted);
    public static event ActivetedStickHandle activetedStick;

    void OnEnable()
    {
        QuestionManagerBilliard.validededQuestion += OnShowScreenFeedback;
    }

    void Start()
    {
        screen.SetActive(false);
    }

    public void OnQuestionGeneratedBilliard(string msg)
    {
        QuestionText.text = msg;
    }

    public void OnShowScreenFeedback(bool msg)
    {
        Time.timeScale = 0f;
        screen.SetActive(true);
        textFeedBack.text = msg ? messageFeedBack[0] : messageFeedBack[1];
        emojiFeedBack.sprite = msg  ? emoji[0] : emoji[1];
    }

    public void resetScene()
    {
        Time.timeScale = 1f;
        screen.SetActive(false);
        activetedStick?.Invoke(true);
    }
}
