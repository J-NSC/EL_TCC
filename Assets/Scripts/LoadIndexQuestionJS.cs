using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIndexQuestionJS : MonoBehaviour
{
    [SerializeField] QuizIndexSO quizIndex;
    [SerializeField] TextAsset jsonFile;

    void OnEnable()
    {
        Door.savaDataIndex += SaveJsonData;
    }
    
    void OnDisable()
    {
        Door.savaDataIndex -= SaveJsonData;
    }

    void Start()
    {
    }

    void Update()
    {
        LoadJsonData();  
    }

    void LoadJsonData()
    {
        if (jsonFile != null)
        {
            string json = jsonFile.ToString();
            JsonUtility.FromJsonOverwrite(json, quizIndex);
        }
    }

    void SaveJsonData()
    {
        if (quizIndex != null)
        {
            string path = Application.dataPath + "/indexJson.json";
            string json = JsonUtility.ToJson(quizIndex);
            
            File.WriteAllText(path, json);
        }
    }
}
