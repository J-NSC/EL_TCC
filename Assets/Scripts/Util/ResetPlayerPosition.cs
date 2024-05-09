using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPosition : MonoBehaviour
{
    [SerializeField] GameObject spwanerPosition;
    [SerializeField] GameObject player;
    [SerializeField] Animator fadeAnim;
    [SerializeField] Image fadeImage;

    [SerializeField] Vector3 newPos;

    void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    void OnEnable()
    {
        PlayerCollider.resetPlayerPosition += OnResetPlayerPosition;
        PlayerCollider.ChangedPositionSpwaner += OnchagedSpwanerPosition;
    }


    void OnDisable()
    {
        PlayerCollider.resetPlayerPosition -= OnResetPlayerPosition;
        PlayerCollider.ChangedPositionSpwaner -= OnchagedSpwanerPosition;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnResetPlayerPosition()
    {
        StartCoroutine(StartFade());
    }

    void OnchagedSpwanerPosition()
    {
        spwanerPosition.transform.position = newPos;
    }

    IEnumerator StartFade()
    {
        fadeAnim.SetBool("Fade", true);
        Debug.Log("teste");
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        fadeAnim.SetBool("Fade", false);
        player.transform.position = spwanerPosition.transform.position;
    }
    
}
