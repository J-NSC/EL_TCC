using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainer;
    public FloatValue playerCurrentHeart;
    
    
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainer.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }        
    }

    public void UpdateHearts()
    {
        float tempHeartlh = playerCurrentHeart.RuntimeValue / 2;
        for (int i = 0; i < heartContainer.initialValue; i++)
        {
            if (i<= tempHeartlh -1)
            {
                hearts[i].sprite = fullHeart;
            }else if (i >= tempHeartlh)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
