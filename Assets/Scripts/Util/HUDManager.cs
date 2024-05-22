using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text QuestionText;

    [Header("FeedBackSrcreen")] 
    [SerializeField] GameObject screen; 
    [SerializeField] Image emojiFeedBack;
    [SerializeField] Image ButtonFeedBackImage;
    [SerializeField] TMP_Text textFeedBack;
    [SerializeField] TMP_Text textButtoFeedBack;
    
    [SerializeField] string[] buttonFeedBack;
    [SerializeField] Sprite[] buttonFeedBackSprite;
    [SerializeField] Sprite[] emoji;
    [SerializeField] string[] messageFeedBack;

    [Header("Score")]
    [SerializeField] TMP_Text scoreText;

    [Header("GameOver")] 
    [SerializeField] GameObject gameOverScreen;

    bool isGameOver = false;
    
    public delegate void ActivetedStickHandle(bool activeted);
    public static event ActivetedStickHandle activetedStick;
    
    [SerializeField] GameObject[] MenuButtons ;


    void OnEnable()
    {

        QuizManage.gameOver += GameOverScreen;

        QuestionManagerBilliard.gameOver += GameOverScreen;
        
        GameManager.gameOver += GameOverScreen;

        QuestionManagerBilliard.SendScoreBilliard += msg =>
        {
            scoreText.text = msg;
        };
        
        QuizManage.scoreSend += (msg) =>
        {
            scoreText.text = msg;
        };
        
        QuestionManagerBilliard.validededQuestion += OnShowScreenFeedback;
    }

    void OnDisable()
    {
        QuizManage.gameOver -= GameOverScreen;
        QuestionManagerBilliard.gameOver -= GameOverScreen;
        GameManager.gameOver -= GameOverScreen;
        QuestionManagerBilliard.validededQuestion -= OnShowScreenFeedback;

    }

    void Start()
    {
        isGameOver = false;
        gameOverScreen.SetActive(false);
        screen.SetActive(false);
    }

    public void OnQuestionGeneratedBilliard(string msg)
    {
        QuestionText.text = msg;
    }

    public void OnShowScreenFeedback(bool msg)
    {
        if (!isGameOver)
        {
            PauseGame(0);
            screen.SetActive(true);
            textFeedBack.text = msg ? messageFeedBack[0] : messageFeedBack[1];
            emojiFeedBack.sprite = msg ? emoji[0] : emoji[1];
            textButtoFeedBack.text = msg ? buttonFeedBack[0] : buttonFeedBack[1];
            ButtonFeedBackImage.sprite = msg ? buttonFeedBackSprite[0] : buttonFeedBackSprite[1];
        }
            
    }

    public void resetScene()
    {
        PauseGame(1);
        screen.SetActive(false);
        activetedStick?.Invoke(true);
    }

    void PauseGame(float value)
    {
        Time.timeScale = value;
    }

    void GameOverScreen()
    {
        Debug.Log("fim de jogo");
        isGameOver = true;
        MenuButtons[0].SetActive(false);
        MenuButtons[1].SetActive(true);
        gameOverScreen.SetActive(true);
    }

    public void resetMineGame()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }
}
