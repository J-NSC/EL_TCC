using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void TriggeredQuestionAreaHandler(bool actived, string name);
    public static event TriggeredQuestionAreaHandler triggeredQuestionArea;

    public delegate void PlayerEnteredToDoorHandler(bool isDoor);
    public static event PlayerEnteredToDoorHandler playerEnteredToDoor;

    public delegate void PlateShowedHandle(bool showed);
    public static event PlateShowedHandle plateShowed;
    
    public delegate void ChangedLayerToPlatformHandle(string layer);
    public static event ChangedLayerToPlatformHandle changedLayer;

    public delegate void SendNameHouseHandle(string name, bool actived);
    public static event SendNameHouseHandle sendNameHouse;

    public delegate void RestePlayerPosition();
    public static event RestePlayerPosition resetPlayerPosition;
    
    public delegate void ChangedPositionSpwanerHandle();
    public static event ChangedPositionSpwanerHandle ChangedPositionSpwaner;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            triggeredQuestionArea?.Invoke(true,other.gameObject.name);
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
        
        if (other.gameObject.CompareTag("Platforme"))
        {
            changedLayer?.Invoke("Plataform");
        }

        if (other.gameObject.CompareTag("MiniGameHouse"))
        {
            Debug.Log("teste");
            sendNameHouse?.Invoke(other.name, true);
        }

        if (other.gameObject.CompareTag("FallMap"))
        {
            resetPlayerPosition?.Invoke();
        }

        if (other.gameObject.CompareTag("SpwanerMov"))
        {
            ChangedPositionSpwaner?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            triggeredQuestionArea?.Invoke(true,other.gameObject.name);
        }
        if (other.gameObject.CompareTag("Door"))
        {
            playerEnteredToDoor?.Invoke(false);
        }

        if (other.gameObject.CompareTag("InfoBox"))
        {
            plateShowed?.Invoke(false);
        }
        
        if (other.gameObject.CompareTag("Platforme"))
        {
            StartCoroutine(ChangedLayer());
        }
        
        if (other.gameObject.CompareTag("MiniGameHouse"))
        {
            sendNameHouse?.Invoke(other.name, false);
        }
    }

    IEnumerator ChangedLayer()
    {
        yield return new WaitForSeconds(.2f);
        changedLayer?.Invoke("Ground");
    }
}