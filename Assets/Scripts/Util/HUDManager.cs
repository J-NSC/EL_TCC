using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text QuestionText;
    
    void Start()
    {
    
    }

    public void OnQuestionGeneratedBilliard(string msg)
    {
        QuestionText.text = msg;
    }
}
