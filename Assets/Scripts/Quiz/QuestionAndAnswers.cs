using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsQuiz", menuName = "Question/Quiz")]
public class QuestionAndAnswers: ScriptableObject
{
    public string questionName;
    public string question;
    public string[] answers;
    public int correctAnswer;
}
