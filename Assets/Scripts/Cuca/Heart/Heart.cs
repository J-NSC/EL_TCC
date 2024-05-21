using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HeartStatus
{
    Empty = 0,
    Full = 1,
    
}

public class Heart : MonoBehaviour
{
    
    [SerializeField] Sprite FullHeart, emptyHeart;
    Image heartImage;

    void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = FullHeart;
                break;
            
        }
    }
    
}
