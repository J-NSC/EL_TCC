using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnim;
    [SerializeField] SignInfoSO signInfoSo;
    public  delegate void ChangedSceneHandle(string scene);
    public static event ChangedSceneHandle changedScene;

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
        changedScene?.Invoke(signInfoSo.Minegame);
    }
    
}
