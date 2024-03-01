using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatas", menuName = "Game/Datas")]
public class GameDataSO : ScriptableObject
{
    public Difficulty difficulty;
    public int rows;
    public int colums;

    public Sprite backGround;
    public int preferrendTopBottomPadding;
    public Vector2 spacing;
}
