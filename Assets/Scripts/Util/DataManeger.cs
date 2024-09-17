using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManeger : MonoBehaviour
{
    [SerializeField] CharacterStatisticSO character; 
    
    void OnEnable()
    {
        ScenesManager.resetPrefab += OnResetPrefab;
    }

    void OnDisable()
    {
        ScenesManager.resetPrefab -= OnResetPrefab;
    }

    void OnResetPrefab()
    {
        character.loadLevel = 0;
        PlayerPrefs.DeleteAll();
    }
}
