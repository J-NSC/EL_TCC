using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetQuestionTest : MonoBehaviour
{
    [SerializeField] private TMP_Text teste;
    // [SerializeField] private GameObject prefabUi;
    List<GameObject> uiElements = new List<GameObject>();

    private void OnEnable()
    {
        InputHandle.onAnswerHandler += UpdateUi;
    }

    private void OnDisable()
    {
        InputHandle.onAnswerHandler -= UpdateUi;
    }
    void Update()
    {

    }

    private void UpdateUi(List<Questions> questions)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            Questions q = questions[i];
            // if( i >= uiElements.Count ){
            //     var inst = Instantiate(prefabUi, Vector3.zero,Quaternion.identity );
            //     inst.transform.SetParent( gameObject.transform,false);
            // }
            // teste.text = q.question;
            Debug.Log(q.question + " " + q.feedback);
            teste.text = q.feedback;
        }

    }
}

