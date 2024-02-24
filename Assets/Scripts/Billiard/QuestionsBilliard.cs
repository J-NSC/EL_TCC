using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsBilliard", menuName = "Question/Billiard")]
public class QuestionsBilliard : ScriptableObject
{
    public string questionName;
    public string question;
    public string correctAnswer;
}
