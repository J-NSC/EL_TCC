using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaManage : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO cardCollectionSo;
    public GameDataSO gameDatas;

    CardGridGenerator cardGridGenerator;
    List<CardController> cardControllers;
    
    void Awake()
    {
        cardControllers = new List<CardController>();

        cardGridGenerator = new CardGridGenerator(cardCollectionSo, gameDatas);
        SetCardGridLayoutParams();
        GenerateCards();

        GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));

    }

    void GenerateCards()
    {
        int cardCount = gameDatas.rows * gameDatas.colums;
        for (int i = 0; i < cardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab, transform);
            card.transform.name = $"Card({i.ToString()})";
        }
    }

    void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout = GetComponent<CardGridLayout>();

        cardGridLayout.preferredPadding = gameDatas.preferrendTopBottomPadding;
        cardGridLayout.rows = gameDatas.rows;
        cardGridLayout.columns = gameDatas.colums;
        cardGridLayout.spacing = gameDatas.spacing;
    }
}
