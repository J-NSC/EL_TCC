using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreateScriptObject : MonoBehaviour
{
    [SerializeField] TextAsset questionsFile ;
    
    void Awake()
    {
       QuizDataLoad();
    }

    void QuizDataLoad()
    {
        if (questionsFile != null)
        {
            string json = questionsFile.text;

            QuestionsList questionsList = JsonUtility.FromJson<QuestionsList>(json);
            
            foreach (QuestionData questionData in questionsList.questions)
            {
                QuestionAndAnswers questionObject = ScriptableObject.CreateInstance<QuestionAndAnswers>();
                questionObject.questionName = questionData.questionName;
                questionObject.question = questionData.question;
                questionObject.answers = questionData.answers;
                questionObject.correctAnswer = questionData.correctAnswer;

                // Salva o ScriptableObject em um arquivo Asset com o nome da questão
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(questionObject, $"Assets/Resources/ScriptObject/Quiz/{questionObject.questionName}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
#endif
                
            }
        }
        else
        {
            Debug.LogError("O arquivo não foi atribuido ao TextAsset no inspector");
        }
    }
}

[System.Serializable]
public class QuestionData
{
    public string questionName;
    public string question;
    public string[] answers;
    public int correctAnswer;
}

[System.Serializable]
public class QuestionsList
{
    public QuestionData[] questions;
}

