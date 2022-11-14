using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;// question and answers
    [SerializeField] TextMeshProUGUI questionText; // canvas text object use to hold text

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons; // each button has a child obj of text
    int correctAnswerIndex;
    bool hasAnsweredEarly; //false = timeout default = false
                           //let us check if the timer runout (no choice has made) and we should display the answer. or we had clicked the bottom and should showed the answer.

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField]
    Slider progressBar;
    public bool isComplete = false;


    void Start()
    {
        timer = FindObjectOfType<Timer>();

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.SetTotalQuestion(questions.Count);

        progressBar.maxValue = questions.Count;
        progressBar.minValue = 0;

    }

    void Update()
    {
        // change of image type mer fillAmount
        timerImage.fillAmount = timer.fillFraction;


        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;

            }
            hasAnsweredEarly = false; //set back to default state no timeout occured.
            GetNextQuestion(); // reset the quiz canvas 
            timer.loadNextQuestion = false; // tell timer to continue.

        } // we want to check if answer early or  timeout? and what state timer is in answering/Revealing state?
        else if (!timer.isAnsweringQuestion && !hasAnsweredEarly) // we are in an reveal answer state and timeout.
        {
            //display results
            DisplayAnswer(-1); // Industry Practice to make sure wrong answer is selectd.
            SetButtonState(false);
            // when answertime timeout it will reset the state by setting timer.loadNextQuestion = true begin loading next question.



        }



    }

    public void OnAnswerSelected(int answerIndex)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(answerIndex);
        SetButtonState(false);
        timer.CancelTimer();

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        //progressBar.setQuestionAnswered(questions.Count);



    }

    private void DisplayAnswer(int answerIndex)
    {
        Image buttonImage;

        if (answerIndex == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct";
            scoreKeeper.IncrementCorrectAnswers();
            buttonImage = answerButtons[answerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;

        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Sorry, the correct answer was;\n" + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;

        }
    }

    void GetNextQuestion() // for now it just relaod the same question.
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
        }

    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            //Button button = answerButtons[i].GetComponentInChildren<Button>();
            //button.image.sprite = defaultAnswerSprite;
            //can directly access the image 
            Image image = answerButtons[i].GetComponentInChildren<Image>();
            image.sprite = defaultAnswerSprite;


        }

    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>(); // return the first child of type xx
            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponentInChildren<Button>();
            button.interactable = state;
        }
    }

}
