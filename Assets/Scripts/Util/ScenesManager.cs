using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public delegate void InstantiededPlayerInPointHandler();
    public static event InstantiededPlayerInPointHandler InstantiededPlayer;
    
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Fade;
    [SerializeField] Image fadeImage;
    [SerializeField] Animator fadeAnim;
    [SerializeField] List<int> targetScene;
    
    bool isPaused = false;

    void OnEnable()
    {
        Door.changedScene += Load;
        GameManager.pausedScreen += PauseGame;
        SceneManager.sceneLoaded += OnSceneLoad;

    }

    void OnDisable()
    {
        Door.changedScene -= Load;
        GameManager.pausedScreen -= PauseGame;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void Awake()
    {
        Fade = GameObject.Find("Fade");
        fadeImage = Fade.GetComponent<Image>();
    }

    void Start()
    {
        Menu.SetActive(false);
    }

   

    public void Load(string sceneName)
    {
        StartCoroutine(fading(sceneName));
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (targetScene.Contains(scene.buildIndex))
        {
            InstantiededPlayer?.Invoke();
        } 
    }


    IEnumerator fading(string sceneName)
    {
        fadeAnim.SetBool("Fade", true);
        PauseGame(false);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
    
   

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            Menu.SetActive(true);    
        }
        else
        {
            Time.timeScale = 1f;
            Menu.SetActive(false);
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
