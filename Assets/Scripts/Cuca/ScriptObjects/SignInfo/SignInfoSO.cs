using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "MineGame", menuName = "Scene/MineGames")]
public class SignInfoSO : ScriptableObject
{
    public string HouseName;
    public string Minegame;
}
