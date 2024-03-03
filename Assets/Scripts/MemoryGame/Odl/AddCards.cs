using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{
    [SerializeField] Transform cardField;
    [SerializeField] GameObject card;
    [SerializeField] Sprite teste;
    public int numMax = 8;
    
    void Awake()
    {

        teste = Resources.Load<Sprite>("Images/IMGMemoryGame/alter-do-chao");
        for (int i = 0; i < numMax; i++)
        {
            GameObject _card = Instantiate(card, cardField, false);
            _card.name = "" + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
