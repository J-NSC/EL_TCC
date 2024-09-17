using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

  
    CharacterStatisticSO characterSO;
    [SerializeField] Vector3 currentPosition;

    string keyX = "PositionX";
    string keyY = "PositionY";
    string keyZ = "PositionZ";

    void OnEnable()
    {
        Player.setPosition += SetPosition;
        Player.loadPosition += LoadPosition;
    }

    void OnDisable()
    {
        Player.setPosition -= SetPosition;
        Player.loadPosition -= LoadPosition;
    }

    void SetPosition(Vector3 pos)
    {
        currentPosition = pos;
        SavePosition();
    }
    
    void SavePosition()
    {
        // PlayerPrefs.DeleteKey([keyX,keyY,]);
        PlayerPrefs.SetFloat(keyX, currentPosition.x);
        PlayerPrefs.SetFloat(keyY, currentPosition.y);
        PlayerPrefs.SetFloat(keyZ, currentPosition.z);
        PlayerPrefs.Save();
    }

    Vector3 LoadPosition()
    {
        Vector3 position = new Vector3();
        if (PlayerPrefs.HasKey(keyX) && PlayerPrefs.HasKey(keyY) && PlayerPrefs.HasKey(keyZ))
        {
            float x = PlayerPrefs.GetFloat(keyX);
            float y = PlayerPrefs.GetFloat(keyY);
            float z = PlayerPrefs.GetFloat(keyZ);
            position = new Vector3(x, y, z);

        }
        return position;
    }
}
