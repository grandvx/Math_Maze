using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Text questionText;
    public List<Button> answerButtons;

    public QuizQuestionSet questionSet; //Initialize questions

    private int currentQuestionIndex;
    private List<Question> questions;

    private bool quizCompleted;

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public List<string> answerOptions;
        public int correctAnswerIndex;
    }

    private void Start()
    {
        //InitializeQuestions();

        // Display the first question
        
    }

    public void StartQuiz()
    {
        // Reset quiz-related variables and start displaying the first question
        currentQuestionIndex = 0;
        quizCompleted = false;
        InitializeQuestions();
        ShowQuestion(currentQuestionIndex);
    }

    private void InitializeQuestions()
    {

        if (questionSet != null)
        {
            questions = ConvertToQuizControllerQuestions(questionSet.questions);
        }
    }

private List<Question> ConvertToQuizControllerQuestions(List<QuizQuestion> quizQuestions)
    {
        List<Question> convertedQuestions = new List<Question>();

        foreach (var quizQuestion in quizQuestions)
        {
            Question convertedQuestion = new Question
            {
                questionText = quizQuestion.questionText,
                answerOptions = quizQuestion.answerOptions,
                correctAnswerIndex = quizQuestion.correctAnswerIndex
            };

            convertedQuestions.Add(convertedQuestion);
        }

        return convertedQuestions;
    }

    private void ShowQuestion(int questionIndex)
    {
        if (questionIndex >= 0 && questionIndex < questions.Count)
        {
            Question currentQuestion = questions[questionIndex];

            // Display the question text
            questionText.text = currentQuestion.questionText;

            // Display the answer options on buttons
            for (int i = 0; i < answerButtons.Count; i++)
            {
                if (i < currentQuestion.answerOptions.Count)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answerOptions[i];

                    int answerIndex = i; // Capture the current value of i
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex, currentQuestion.correctAnswerIndex));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Question index out of range.");
        }
    }

    public bool IsQuizAnsweredCorrectly()
    {
        return quizCompleted;
    }

    private void OnAnswerSelected(int selectedAnswerIndex, int correctAnswerIndex)
    {
        if (quizCompleted)
        {
            Debug.Log("Quiz already completed.");
            quizCompleted = true;
            return ;
        }

        if (selectedAnswerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");

            // Advance to the next question
            currentQuestionIndex++;

            if (currentQuestionIndex < questions.Count)
            {
                // Display the next question
                ShowQuestion(currentQuestionIndex);
            }
            else
            {
                Debug.Log("Quiz completed!");
                quizCompleted = true;
                return ;
            }
        }
        else
        {
            Debug.Log("Wrong Answer. Try again.");
        }
    }
}
