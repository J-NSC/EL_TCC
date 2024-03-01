using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardCollection", menuName = "Card/CardCollection")]
public class CardCollectionSO : ScriptableObject
{
    public List<CardScriptObject> cards;
}
