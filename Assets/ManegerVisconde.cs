using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegerVisconde : MonoBehaviour
{
    [SerializeField] Sing[] sing;

    [SerializeField] QuizManagerCuca[] quizManagerCuca;

    void Awake()
    {
        sing = FindObjectsOfType<Sing>();
        Destroy(sing[1].gameObject);

        quizManagerCuca = FindObjectsOfType<QuizManagerCuca>();
        Destroy(quizManagerCuca[1].gameObject);
        

    }   
}
