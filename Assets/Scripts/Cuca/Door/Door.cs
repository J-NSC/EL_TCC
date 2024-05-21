using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] SignInfoSO signInfoSo;
    public  delegate void ChangedSceneHandle(int indexscene);
    public static event ChangedSceneHandle changedScene;

    public delegate void SaveDataIndex();
    public static event SaveDataIndex savaDataIndex;
    
    void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Player.openDoor += DoorAnim;
    }

    void OnDisable()
    {
        Player.openDoor -= DoorAnim;
    }


    void DoorAnim(bool isOpen)
    {
        doorAnim.SetBool("IsOpen", isOpen);
    }

    void endOpenDoorAnimation()
    {
        changedScene?.Invoke(signInfoSo.MinegameIndex);
    }

    void saveDataInJson()
    {
        savaDataIndex?.Invoke();
    }
}
