using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{
    begin, 
    pause,
    gameOver
}

public class GameManager : MonoBehaviour
{
    [Header("POOL")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    
    [Header("MemoryGame")]
    [SerializeField] string filename;
    public Sprite[] cards;

    public static GameManager inst;

    void Awake() {
        if(inst == null)
            inst = this;

        cards = Resources.LoadAll<Sprite>("Images/IMGMemoryGame");
        
    }

    void Update()
    {
    }

    void Lose(string message)
    {
        
    }

    void Win(string message)
    {
        
    }

    
}
