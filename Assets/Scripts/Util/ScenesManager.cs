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
    
    public delegate void ResededPlayerSpwanerHandler();
    public static event ResededPlayerSpwanerHandler resededPlayerSpwaner;
    
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Fade;
    [SerializeField] Image fadeImage;
    [SerializeField] Animator fadeAnim;
    [SerializeField] List<int> targetScene;

    [SerializeField] GameObject[] MenuButtons ;
    
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

    }

    void OnDisable()
    {
        Door.changedScene -= Load;
        GameManager.pausedScreen -= PauseGame;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void Start()
    {
        Menu.SetActive(false);
    }

   

    public void Load(int Indexscene)
    {
        StartCoroutine(fading(Indexscene));
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (targetScene.Contains(scene.buildIndex))
        {
            InstantiededPlayer?.Invoke();
        } 
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
            MenuButtons[0].SetActive(true);
            MenuButtons[1].SetActive(false);
            Menu.SetActive(true);    
        }
        else
        {
            MenuButtons[0].SetActive(false);
            Time.timeScale = 1f;
            Menu.SetActive(false);
        }
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
