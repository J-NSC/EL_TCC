using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void TriggeredQuestionAreaHandler();
    public static event TriggeredQuestionAreaHandler triggeredQuestionArea;
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            triggeredQuestionArea?.Invoke();
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
        }
    }
}
