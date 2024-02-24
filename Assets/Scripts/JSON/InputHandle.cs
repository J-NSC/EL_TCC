using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.IO.LowLevel.Unsafe;

public class InputHandle : MonoBehaviour
{
    [SerializeField] TMP_InputField questionInput;
    [SerializeField] TMP_InputField feedBackInput;
    [SerializeField] string filename;

    List<Questions> questions = new List<Questions>();

    public delegate void OnAnswerHandler(List<Questions> questions);
    public static event OnAnswerHandler onAnswerHandler;

    private void Start() {
        questions = FIleHandle.ReadFromJSON<Questions>(filename);

        if(onAnswerHandler != null){
            onAnswerHandler.Invoke(questions);
        }
        
    }

    public void AddNameToList(){
        questions.Add(new Questions(questionInput.text, feedBackInput.text));
        questionInput.text = "";
        feedBackInput.text = "";
        
        FIleHandle.SaveToJSON<Questions>(questions, filename);
        
    }
}
