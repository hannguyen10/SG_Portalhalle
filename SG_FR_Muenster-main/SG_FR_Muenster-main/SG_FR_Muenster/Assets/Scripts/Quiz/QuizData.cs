using UnityEngine;

[CreateAssetMenu(menuName = "QuizData")]
public class QuizData : ScriptableObject
{
    [TextArea]
    public string question;

    public string[] answers;

    [Tooltip("0, 1, 2")]
    public int correctAnswerIndex;

    [TextArea]
    public string correctExplanation;

    [TextArea]
    public string wrongExplanation;
}
