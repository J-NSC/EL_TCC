using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{

    [SerializeField] GameObject Menu;
    [SerializeField] GameObject fade;

    void Start()
    {
        Menu.SetActive(false);
        fade.SetActive(false);
    }

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
        fade.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);
        fade.SetActive(false);
    }
}
