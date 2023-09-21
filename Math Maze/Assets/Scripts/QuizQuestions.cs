using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Question Set", menuName = "Quiz Question Set")]
public class QuizQuestionSet : ScriptableObject
{
    public List<QuizQuestion> questions = new List<QuizQuestion>
    {
        new QuizQuestion
        {
            questionText = "What is the capital of France?",
            answerOptions = new List<string>
            {
                "London",
                "Madrid",
                "Paris",
                "Berlin"
            },
            correctAnswerIndex = 2
        },
        new QuizQuestion
        {
            questionText = "Which planet is known as the Red Planet?",
            answerOptions = new List<string>
            {
                "Earth",
                "Mars",
                "Venus",
                "Jupiter"
            },
            correctAnswerIndex = 1
        },
        // Add more questions here
    };
}

[System.Serializable]
public class QuizQuestion
{
    public string questionText;
    public List<string> answerOptions;
    public int correctAnswerIndex;
}
