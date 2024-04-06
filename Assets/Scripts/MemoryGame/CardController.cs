using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerDownHandler
{
	public delegate void InfoBoxShowedHandle(string description, string name);
	public static event InfoBoxShowedHandle InfoBoxShowed;

	public delegate void ButtonDescriptionHandle(Button button);
	public static event ButtonDescriptionHandle buttonDescription;
	
	
	[SerializeField] Image frontFace;
	[SerializeField] Image backFace;
	[SerializeField] Image touristSpot;
	[SerializeField] Button InfoButton;
	[SerializeField] TMP_Text nameSpot;
	[SerializeField] Button DescriptionButton;
	[SerializeField] bool interativeCard = true;
	[SerializeField] InfoButton buttonInfo;

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
		buttonInfo = FindObjectOfType<InfoButton>();
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		DescriptionButton = GetComponentInChildren<Button>(true);
		
		buttonDescription?.Invoke(DescriptionButton);
		

		frontState = new FrontState(this);
		backState = new BackState(this);
		flippingState = new FlippingState(this);
		backFlippingState = new BackFlippingState(this);
		hideAwayState = new HideAwayState(this);

		actualState = backState;
	}

	// Update is called once per frame
	void Update()
	{
		actualState.UpdateActivity();
	}

	internal void SetCardDatas(Sprite background, Sprite front,CardScriptObject card)
	{
		cardType = card;

		frontFace.sprite = front;
		backFace.sprite = background;
		touristSpot.sprite = card.sprite;
		
		nameSpot.text = Regex.Replace(card.nameCard, @"\d", "");

		backFace.gameObject.SetActive(true);
		frontFace.gameObject.SetActive(false);
		touristSpot.gameObject.SetActive(false);
		InfoButton.gameObject.SetActive(false);
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
		touristSpot.gameObject.SetActive(!touristSpot.gameObject.activeSelf);
		nameSpot.gameObject.SetActive(!nameSpot.gameObject.activeSelf);
		InfoButton.gameObject.SetActive(!InfoButton.gameObject.activeSelf);
	}

	public void InactivateCard()
	{
		
		Image cardImage = GetComponent<Image>();
		Color newColor = cardImage.color;
		newColor.a = 0.6f;
		
		backFace.color = newColor;
		frontFace.color = newColor;
		touristSpot.color = newColor;

		
		interativeCard = false;
	}

	public void ChangeScale(float newScale)
	{
		transform.localScale = new Vector3(newScale, 1, 1);
	}

	public void Flip()
	{
		if (interativeCard)
		{
			if (backFace.gameObject.activeSelf)
			{
				cardScale -= (flipSpeed * Time.deltaTime);
				ChangeScale(cardScale);
				if (flipTolerance > cardScale)
				{
					SwitchFaces();
				}
			}
			else
			{
				cardScale +=  (flipSpeed * Time.deltaTime);
				ChangeScale(cardScale);

				if(cardScale >= 1.0f)
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
		if (interativeCard)
		{
			if (!backFace.gameObject.activeSelf)
			{
				cardScale -=  (flipSpeed * Time.deltaTime);
				ChangeScale(cardScale);
				if (flipTolerance > cardScale)
				{
					SwitchFaces();
				}
			}
			else
			{
				cardScale +=  (flipSpeed * Time.deltaTime);
				ChangeScale(cardScale);

				if (cardScale >= 1.0f)
				{
					ChangeScale(1.0f);
					TransitionState(backState);
				}
			}
		}
	
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		actualState.OnClickAction();
	}

	public void ClickedButton()
	{
		InfoBoxShowed?.Invoke(cardType.description, cardType.nameCard);
	}
}
