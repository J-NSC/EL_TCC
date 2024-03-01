using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiliardCreteSciptObject : MonoBehaviour
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
            Debug.Log(json);

            BilliardQuestionsList questionsList = JsonUtility.FromJson<BilliardQuestionsList>(json);
            
            foreach (BilliardQuestionData questionData in questionsList.questions)
            {
                QuestionsBilliard questionObject = ScriptableObject.CreateInstance<QuestionsBilliard>();
                questionObject.questionName = questionData.questionName;
                questionObject.question = questionData.question;
                questionObject.correctAnswer = questionData.correctAnswer;

                // Salva o ScriptableObject em um arquivo Asset com o nome da questão
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(questionObject, $"Assets/Resources/ScriptObject/Bilhar/{questionObject.questionName}.asset");
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
public class BilliardQuestionData
{
    public string questionName;
    public string question;
    public string correctAnswer;
}

[System.Serializable]
public class BilliardQuestionsList
{
    public BilliardQuestionData[] questions;
}
