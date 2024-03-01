using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerDownHandler
{
    public Image frontFace;
    public Image backFace;

    public CardScriptObject cardType;

    GameManager gameManager;

    public CardState actualState;
    public FrontState frontState;
    public BackState backState;
    public FlippingState flippingState;
    public BackFlippingState backFlippingState;
    public MemorizeState memorizeState;
    public HideAwayState hideAwayState;

    float cardScale = 1.0f;
    float flipSpeed = 2.0f;
    float flipTolerance = 0.05f;
        
    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));

        frontState = new FrontState(this);
        backState = new BackState(this);
        flippingState = new FlippingState(this);
        backFlippingState = new BackFlippingState(this);
        hideAwayState = new HideAwayState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        actualState.UpdateActivity();
    }

    internal void SetCardDatas(Sprite background, CardScriptObject card)
    {
        cardType = card;
        frontFace.sprite = card.sprite;
        backFace.sprite = background;
        
        backFace.gameObject.SetActive(true);
        frontFace.gameObject.SetActive(false);
    }

    public void TransitionState(CardState newState)
    {
        actualState.EndState();
        actualState = newState;
        actualState.EnterState();
    }

    public void SwitchFaces()
    {
        backFace.gameObject.SetActive(!backFace.gameObject.activeSelf);
        frontFace.gameObject.SetActive(!frontFace.gameObject.activeSelf);
    }

    public void InactivateCard()
    {
        backFace.gameObject.SetActive(false);
        frontFace.gameObject.SetActive(false);

        Image cardImage = GetComponent<Image>();
        Color newColor = cardImage.color;
        newColor.a = 0.0f;
        cardImage.color = new Color();
    }

    public void ChangeScale(float newScale)
    {
        transform.localScale = new Vector3(newScale, 1, 1);
    }

    public void Flip()
    {
        if (backFace.gameObject.activeSelf == true)
        {
            cardScale = cardScale - (flipSpeed * Time.deltaTime);
            ChangeScale(cardScale);

            if (flipTolerance > cardScale)
            {
                SwitchFaces();
            }
            else
            {
                cardScale = cardScale + (flipSpeed * Time.deltaTime);
                
                ChangeScale(cardScale);

                if (cardScale >= 1.0f)
                {
                    ChangeScale(1.0f);
                    TransitionState(frontState);
                    gameManager.SetSelectedCard(gameObject);
                }
            }
        }
    }
    
    public void BackFlip()
    {
        //Hide foreground
        if (backFace.gameObject.activeSelf == false)
        {
            cardScale = cardScale - (flipSpeed * Time.deltaTime);
            ChangeScale(cardScale);
            //Show foreground
            if (flipTolerance > cardScale)
            {
                SwitchFaces();
            }
        }
        else
        {
            cardScale = cardScale + (flipSpeed * Time.deltaTime);
            ChangeScale(cardScale);

            if (cardScale >= 1.0f)
            {
                ChangeScale(1.0f);
                TransitionState(backState);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        actualState.OnClickAction();
    }
}
