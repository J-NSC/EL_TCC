using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CucaQuestion", menuName = "CucaGame/Cuca")]
public class SOCucaGame : ScriptableObject
{
    public int id;
    public string questionName;
    public string question;
    public string[] answers;
    public int correctAnswer;
    public bool correct;
}
