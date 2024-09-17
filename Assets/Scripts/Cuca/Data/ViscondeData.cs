using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViscondeData : MonoBehaviour
{
    string KeyPos = "IndexPosVisconde";
    string KeyLine = "IndexLineVisconde";

    void OnEnable()
    {
        Sing.saveIndexPosition += SaveIndexPosition;
        Sing.loadIndexPosition += LoadIndexPosition;
    }

    void OnDisable()
    {
        Sing.saveIndexPosition -= SaveIndexPosition;
        Sing.loadIndexPosition -= LoadIndexPosition;
    }


    void SaveIndexPosition(int posIndex, int lineIndex)
    {
        Debug.Log($"Save data: {posIndex}, {lineIndex}");
        PlayerPrefs.SetInt(KeyPos, posIndex);
        PlayerPrefs.SetInt(KeyLine,lineIndex);
        
    }

    public (int, int) LoadIndexPosition()
    {
        Debug.Log($"Load data");
        return (PlayerPrefs.GetInt(KeyPos), PlayerPrefs.GetInt(KeyLine));
    }
}
