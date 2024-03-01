using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardObject", menuName = "Card/location")]
public class CardScriptObject : ScriptableObject
{
   public string nameCard;
   public string pairName;
   public Sprite sprite;
   public string description;

   public bool IsPair(string giveName)
   {
      giveName = giveName.ToLower();
      return (giveName == pairName);
   }
      
      
   
   
}
