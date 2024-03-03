using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public GameObject infoBox;
    public TMP_Text descriptionText;
    public TMP_Text nameSpotText;
    public string description;
    public string nameSpot;
    bool WriteDescription;
    
    void OnEnable()
    {
        CardController.InfoBoxShowed += (descriptio, name) =>
        {
            description = descriptio;
            nameSpot = name;
        };

        CardController.buttonDescription += button =>
        {
            button.onClick.AddListener(ShowInfoSpot);
        };
    }

    void Awake()
    {
        infoBox = GameObject.FindWithTag("InfoBox");
    }

    void Start()
    {
        infoBox.SetActive(false);
        Debug.Log(descriptionText.gameObject);
    }

    void Update()
    {
        if (WriteDescription)
        {
            descriptionText.text = description;
            nameSpotText.text = Regex.Replace( nameSpot , @"\d", "");
            WriteDescription = false;
        }
    }

    public void ShowInfoSpot()
    {
       infoBox.SetActive(true);
       WriteDescription = true;
    }

    public void HindeInfoSpot()
    {
        infoBox.SetActive(false);

        descriptionText.text = " ";
    }
}
