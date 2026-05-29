using UnityEngine;

[CreateAssetMenu(menuName = "QuizData")]
// saveable data file
public class QuizData : ScriptableObject
{
    [TextArea]
    public string question;

    public string[] answers;

    public int correctAnswerIndex;

    [TextArea]
    public string[] correctExplanationLines;

    [TextArea]
    public string[] wrongExplanationLines;

    [Header("Gargulus Sprites")]
    public Sprite correctSprite;
    public Sprite wrongSprite;
}
