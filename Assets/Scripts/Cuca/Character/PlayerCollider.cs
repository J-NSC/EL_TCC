using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void TriggeredQuestionAreaHandler(bool actived);
    public static event TriggeredQuestionAreaHandler triggeredQuestionArea;

    public delegate void PlayerEnteredToDoorHandler(bool isDoor);
    public static event PlayerEnteredToDoorHandler playerEnteredToDoor;

    public delegate void PlateShowedHandle(bool showed);
    public static event PlateShowedHandle plateShowed;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            triggeredQuestionArea?.Invoke(true);
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            playerEnteredToDoor?.Invoke(true);
        }

        if (other.gameObject.CompareTag("InfoBox"))
        {
            plateShowed?.Invoke(true);            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            triggeredQuestionArea?.Invoke(false);
        }
        if (other.gameObject.CompareTag("Door"))
        {
            playerEnteredToDoor?.Invoke(false);
        }

        if (other.gameObject.CompareTag("InfoBox"))
        {
            plateShowed?.Invoke(false);
        }
    }
}
