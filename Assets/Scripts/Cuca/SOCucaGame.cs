using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CucaQuestion", menuName = "CucaGame/Cuca")]
public class SOCucaGame : ScriptableObject
{
    public string LevelName;
    public string Question;
    public string correctAnswer;
}
