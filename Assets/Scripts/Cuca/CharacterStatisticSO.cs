using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/statistic")]
public class CharacterStatisticSO : ScriptableObject
{
    public Vector3 SpwanPoint;
    public int coins;
    public int points;
    
    [Header("Jumper")]
    public int CountJump;
    public int maxJump;
    
    [Header("PowerUps")]
    public List<powerUps> powerUps;
}

[Serializable]
public struct powerUps
{
    public string name;
    public bool actived;
}
