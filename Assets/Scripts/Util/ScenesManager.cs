using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public delegate void InstantiededPlayerInPointHandler();
    public static event InstantiededPlayerInPointHandler InstantiededPlayer;
    
    public delegate void ResetPrefabsDataHandler();
    public static event ResetPrefabsDataHandler resetPrefab;

    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Fade;
    [SerializeField] Image fadeImage;
    [SerializeField] Animator fadeAnim;
    [SerializeField] List<int> targetScene;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text msgMenu;

    [SerializeField] GameObject[] MenuButtons ;

    [SerializeField] bool isGameOver;
    
    bool isPaused = false;
    
    void Awake()
    {
        Fade = GameObject.Find("Fade");
        fadeImage = Fade.GetComponent<Image>();
    }

    void OnEnable()
    {
        Door.changedScene += Load;
        GameManager.pausedScreen += PauseGame;
        SceneManager.sceneLoaded += OnSceneLoad;
        Player.gameOver += GameOverCuca;
    }

    void OnDisable()
    {
        Door.changedScene -= Load;
        GameManager.pausedScreen -= PauseGame;
        SceneManager.sceneLoaded -= OnSceneLoad;
        Player.gameOver -= GameOverCuca;
    }

    void Start()
    {
        Menu.SetActive(false);
    }

    void Update()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Load(int Indexscene)
    {
        if(SceneManager.GetActiveScene().name == "Menu"){
            resetPrefab?.Invoke();
        }
        StartCoroutine(fading(Indexscene));
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (targetScene.Contains(scene.buildIndex))
        {
            InstantiededPlayer?.Invoke();
        } 
        
        Time.timeScale = 1;
        isGameOver = false;
    }

    public void ResetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    
    }

    IEnumerator fading(int Indexscene)
    {
        fadeAnim.SetBool("Fade", true);
        PauseGame(false);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        
        SceneManager.LoadScene(Indexscene);
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            msgMenu.text = "Pause";
            if(SceneManager.GetActiveScene().buildIndex != 1 ){
                MenuButtons[0].SetActive(true);
                MenuButtons[1].SetActive(false);
                MenuButtons[2].SetActive(false);
                MenuButtons[3].SetActive(true);
            }
            else
            {
                MenuButtons[0].SetActive(true);
                MenuButtons[1].SetActive(false);
                MenuButtons[2].SetActive(true);
                MenuButtons[3].SetActive(false);
            }
            Menu.SetActive(true);    
        }
        else
        {
            MenuButtons[0].SetActive(false);
            Time.timeScale = 1f;
            Menu.SetActive(false);
        }
    }

    void GameOverCuca()
    {
        isGameOver = true;
    }
    
    public void ExitGame()
    {
        isGameOver = false;
        Application.Quit();
    }
}
