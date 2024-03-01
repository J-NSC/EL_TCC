using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("POOL")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;

    GameState gameState;

    public CardSelectionState cardSelectionState;
    public PairSelectionState pairSelectionState;
    public MemorizeCardsState memorizeCardsState;
    public MatchingCardsState matchingCardsState;
    public EndGameState endGameState;
    public PauseGameState pauseGameState;

    public GameObject[] selectedCards;

    int cardCount;
    int movesCount;
    
    public static GameManager inst;
    
    public int CardCount
    {
        set { cardCount = value;}
        get { return cardCount;}

    }

    void Awake() {
        if(inst == null)
            inst = this;

        
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
        }
    }

    void InitStates()
    {
        cardSelectionState = new CardSelectionState(this);
        pairSelectionState = new PairSelectionState(this);
        memorizeCardsState = new MemorizeCardsState(this);
        matchingCardsState = new MatchingCardsState(this);
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

        if (selectedCard[0] == null)
        {
            selectedCard[0] = selectedCard;
            TransitionState(pairSelectionState);
        }else if (selectedCard[1] == null)
        {
            selectedCard[1] = selectedCard;

            if (matchingCardsState())
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
        selectedCards[0] == null;
        selectedCards[1] == null;
    }

    bool MatchSelectedCards()
    {
        CardScriptObject first = selectedCards[0].GetComponent<CardController>().cardType;
        CardScriptObject second = selectedCards[1].GetComponent<CardController>().cardType;
        
        return first != null && second != null && first.nameCard == second.pairName && first.pairName == second.nameCard;
    }
}
