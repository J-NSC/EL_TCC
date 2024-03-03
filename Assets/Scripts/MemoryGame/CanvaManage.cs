using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaManage : MonoBehaviour
{
	public GameObject cardPrefab;
	public CardCollectionSO cardCollection;

	public GameDataSO gameDatas;
	public CardGridGenerator cardGridGenerator;
	
	public List<CardController> cardControllers;

	void Awake()
	{
		
		cardControllers = new List<CardController>();

		cardGridGenerator = new CardGridGenerator(cardCollection, gameDatas);

		SetCardGridLayoutParams();
		GenerateCards();

		GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		gameManager.CardCount = gameDatas.rows * gameDatas.columns;
	}

	void SetCardGridLayoutParams()
	{
		CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

		cardGridLayout.preferredPadding = gameDatas.preferredPaddingTopBottom;
		cardGridLayout.rows = gameDatas.rows;
		cardGridLayout.columns = gameDatas.columns;
		cardGridLayout.spacing = gameDatas.spacing;

		cardGridLayout.Invoke("CalculateLayoutInputHorizontal", 0.1f);
	}

	void GenerateCards()
	{
		int cardCount = gameDatas.rows * gameDatas.columns;

		for(int i = 0; i < cardCount; i++)
		{
			GameObject card = Instantiate(cardPrefab, transform);
			card.transform.name = "Card (" + i.ToString() + ")";

			cardControllers.Add(card.GetComponent<CardController>());
		}

		for(int i = 0; i < cardCount/ 2; i++)
		{
			CardScriptObject randomCard = cardGridGenerator.GetRandomAvailableCardSO();
			SetRandomCardToGrid(randomCard);

			CardScriptObject randomCardPair = cardGridGenerator.GetCardPairSO(randomCard.nameCard);
			SetRandomCardToGrid(randomCardPair);
		}
	}
	
	void SetRandomCardToGrid(CardScriptObject randomCard)
	{
		int index = cardGridGenerator.GetRandomCardPositionIndex();
		CardController cardObject = cardControllers[index];
		cardObject.SetCardDatas(gameDatas.background, gameDatas.front ,randomCard);
	}

}
