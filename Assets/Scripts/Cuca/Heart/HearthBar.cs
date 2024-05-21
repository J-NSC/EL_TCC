using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBar : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] float healt, maxHealth;
    List<Heart> hearts = new List<Heart>();


    void Start()
    {
        DrawHearts();
    }

    void DrawHearts()
    {
        ClearHearts();

        float maxHealtRemainder = maxHealth % 2;
        int heartsToMake = (int)((maxHealth / 2) + maxHealtRemainder);

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Math.Clamp(healt - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus) heartStatusRemainder);
        }
    }

    void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<Heart>();
    }
    
}
