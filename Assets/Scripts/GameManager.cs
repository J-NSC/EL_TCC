using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// public UIController uiController;
	public delegate void PausedScreenHandle(bool state);
	public static event PausedScreenHandle pausedScreen;
	
	public delegate void GamOverHandle();
	public static event GamOverHandle gameOver;

	public static GameManager inst;

	GameState gameState;

	public CardSelectionState cardSelectionState;
	public PairSelectionState pairSelectionState;
	public MemorizeCardsState memorizeCardsState;
	public MatchingCardsState matchingCardsState;
	public EndGameState endGameState;
	public PauseGameState pauseGameState;

	public GameObject[] selectedCards;

	public int cardCount = 2;
	int movesCount;
	bool isPause = false;
	bool exitMemoryGame = false;
 

	[Header("Jogo da Cuca")] 
	public LayerMask GroundLayer;
	[SerializeField] CharacterStatisticSO characterSo;
	
	[SerializeField] QuizIndexSO quizIndex;
	
	/*
	 * Trocas as perguntas do quiz
	 */

	public int CardCount
	{
		set
		{
			cardCount = value;
		}
		get
		{
			return cardCount;
		}
	}

	void Awake()
	{
		if (inst == null)
		{
			inst = this;
		}
	}

	void OnEnable()
	{
		QuestionManagerBilliard.enabelDoubleJump += EnabledPowerUp;
	}

	void OnDisable()
	{
		QuestionManagerBilliard.enabelDoubleJump -= EnabledPowerUp;
	}

	void Start()
	{
		movesCount = 0;
		selectedCards = new GameObject[2];
		selectedCards[0] = null;
		selectedCards[1] = null;

		InitStates();
	}

	void Update()
	{
		gameState.UpdateAction();

		if (cardCount <= 0)
		{
			TransitionState(endGameState);
			exitMemoryGame = true;
			
			quizIndex.activedPlatform_2 = true;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;
			pausedScreen?.Invoke(isPause);
		}
		
		if (Input.GetKeyDown(KeyCode.Escape) && exitMemoryGame)
		{
			gameOver?.Invoke();
			exitMemoryGame = false;
		}
	}

	void InitStates()
	{
		cardSelectionState = new CardSelectionState(this);
		pairSelectionState = new PairSelectionState(this);
		memorizeCardsState = new MemorizeCardsState(this, 0.5f);
		matchingCardsState = new MatchingCardsState(this, 0.2f);
		pauseGameState = new PauseGameState(this);
		endGameState = new EndGameState(this);

		gameState = cardSelectionState;
	}

	public void TransitionState(GameState newState)
	{
		gameState.EndState();
		gameState = newState;
		gameState.EnterState();
	}

	public void SetSelectedCard(GameObject selectedCard)
	{
		movesCount++;
		// uiController.ChangeMovesCount(movesCount);

		if (selectedCards[0] == null)
		{
			selectedCards[0] = selectedCard;
			TransitionState(pairSelectionState);
		}
		else if (selectedCards[1] == null)
		{
			selectedCards[1] = selectedCard;

			if (MatchSelectedCards())
			{
				TransitionState(matchingCardsState);
			}
			else
			{
				TransitionState(memorizeCardsState);
			}
		}
	}

	public void RemoveSelectedCards()
	{
		selectedCards[0] = null;
		selectedCards[1] = null;
	}

	bool MatchSelectedCards()
	{
		CardScriptObject first = selectedCards[0].GetComponent<CardController>().cardType;
		CardScriptObject second = selectedCards[1].GetComponent<CardController>().cardType;

		return first != null && second != null && first.nameCard == second.pairName && first.pairName == second.nameCard;
	}

	public void EnabledPowerUp(string name, bool isActived)
	{
		for (int i = 0; i < characterSo.powerUps.Count; i++)
		{
			if (characterSo.powerUps[i].name == name && isActived)
			{
				var teste = characterSo.powerUps[i];
				teste.actived = true;
				characterSo.powerUps[i] = teste;	
			}
		}
	}
}
