using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MemoryGameManager : MonoBehaviour
{
    // [SerializeField] List<CardScriptObject> cards;
    //
    // public List<Animator> anim = new List<Animator>(); 
    // public List<Button> btns = new List<Button>();
    // bool firstGuess, secondGuess;
    // public GameObject[] objects;
    //
    // int countCorrectGuesses;
    // int gameGuesses;
    // public int firstGuessIndex, secondGuessIndex;
    // string firstGuessCard,secondGuessCard;
    //
    // int revealCount = 0;
    //
    // Image img;
    // bool isAnimating = false;
    // int lastClickedIndex = -1;
    //
    // void Start()
    // {
    //     objects = GameObject.FindGameObjectsWithTag("Card");
    //
    //     GetButtons(objects);
    //     AddListeners();
    //     AddGameCard();
    //     Shuffle(cards);
    //     gameGuesses = cards.Count / 2;
    // }
    //
    // void GetButtons(GameObject[] objects)
    // {
    //     
    //     for (int i = 0; i < objects.Length; i++)
    //     {
    //         btns.Add(objects[i].GetComponent<Button>());
    //         anim.Add(objects[i].GetComponent<Animator>());
    //     }
    //     
    // }
    //
    //
    // void AddGameCard()
    // {
    //     int looper = btns.Count;
    //     int index = 0;
    //
    //     for (int i = 0; i < looper; i++)
    //     {
    //         if (index == looper/2)
    //         {
    //             index = 0;
    //         }
    //         gameCard.Add(cards[index]);
    //         index++;
    //     }
    // }
    //
    // void AddListeners()
    // {
    //     foreach (Button btn in btns)
    //     {
    //         btn.onClick.AddListener((() => PickCard()));
    //     }
    // }
    //
    //
    // public void PickCard()
    // {
    //     int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
    //     Debug.Log(index);
    //     if (revealCount >= 2 || isAnimating)
    //     {
    //         return;
    //     }
    //
    //     if (index == lastClickedIndex)
    //     {
    //         StartCoroutine(HideCard(index));
    //         lastClickedIndex = -1;
    //         return;
    //     }
    //
    //     anim[index].SetInteger("State", 1);
    //   
    //     img = objects[index].transform.Find("Local").GetComponent<Image>();
    //     img.sprite = gameCard[index];
    //     
    //     
    //     if (!firstGuess)
    //     {
    //         firstGuess = true;
    //         firstGuessIndex = index;
    //         firstGuessCard = gameCard[firstGuessIndex].name;
    //         
    //     }else if (!secondGuess)
    //     {
    //         secondGuess = true;
    //         secondGuessIndex = index;
    //         secondGuessCard = gameCard[secondGuessIndex].name;
    //         btns[secondGuessIndex].image.sprite = gameCard[secondGuessIndex];
    //
    //         if (firstGuessCard == secondGuessCard)
    //         {
    //             print("X");
    //         }
    //         else
    //         {
    //             print("Y");
    //         }
    //         
    //         StartCoroutine(CheckTheCardMatch());
    //     }
    //
    //     lastClickedIndex = index;
    //     
    //     revealCount++;
    // }
    //
    // IEnumerator HideCard(int index)
    // {
    //     btns[index].interactable = false;
    //     ColorBlock color = btns[index].colors;
    //     color.disabledColor = Color.white;
    //     btns[index].colors = color;
    //     
    //     yield return new WaitForSeconds(0.1f); 
    //
    //     anim[index].SetInteger("State", 2);
    //     
    //     yield return new WaitForSeconds(0.5f);
    //     isAnimating = false;
    //     firstGuess = secondGuess = false;
    //     revealCount -= 2;
    //     btns[index].interactable = true;
    //     color.disabledColor = new Color32(200, 200, 200, 128);
    //     btns[index].colors = color;
    // }
    //
    // IEnumerator CheckTheCardMatch()
    // {
    //     isAnimating = true;
    //     Debug.Log("Heliab");
    //
    //     if (firstGuessCard == secondGuessCard)
    //     {
    //         yield return new WaitForSeconds(1.2f);
    //         btns[firstGuessIndex].interactable = false;
    //         btns[secondGuessIndex].interactable = false;
    //         anim[firstGuessIndex].SetTrigger("Mach");
    //         anim[secondGuessIndex].SetTrigger("Mach");
    //         CheckTheGameFinished();
    //     }
    //     else
    //     {
    //         yield return new WaitForSeconds(1f);
    //         anim[firstGuessIndex].SetInteger("State", 2);
    //         anim[secondGuessIndex].SetInteger("State", 2);
    //     }
    //
    //     yield return new WaitForSeconds(.5f);
    //     isAnimating = false; 
    //     firstGuess = secondGuess = false;
    //     revealCount -= 2;
    // }
    //
    // void CheckTheGameFinished()
    // {
    //     countCorrectGuesses++;
    //     
    //     if(countCorrectGuesses == gameGuesses)
    //         print("fim ");
    //     
    // }
    //
    // void Shuffle(List<Sprite> list){
    //     for (int i = 0; i < list.Count; i++)
    //     {
    //         Sprite temp = list[i];
    //         int randomIndex = Random.Range(i, list.Count);
    //         list[i] = list[randomIndex];
    //         list[randomIndex] = temp;
    //     }
    // }

}
