using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool isAnsweringQuestion; // true = quesiton state false = revealing state.
    public bool loadNextQuestion = false; // false = not ready to load another question yet.
    float timerValue;
    public float fillFraction;
    [SerializeField] Image timerImage;
    [SerializeField] Sprite answerTimeSprite;
    [SerializeField] Sprite questionTimeSprite;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0; // which automatically reset the timer state.
        Debug.Log("Timer cancelled");
    }

    void UpdateTimer() // when the game start it start in else timervlaue = 0 isAnsweringQuestion = flase; then reset the state to initial value.
    {
        timerValue -= Time.deltaTime;

        //changing between answering and showing 
        if (isAnsweringQuestion) // answering question
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
                timerImage.sprite = answerTimeSprite;

            }

        }
        else //showing answer ,not answering question meaning 
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;

            }
            else
            {
                //reset timer. when the game start it start here.!!!!
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion; // ready to show the next question.
                loadNextQuestion = true; // tell Quiz.cs ready to load another question 
                timerImage.sprite = questionTimeSprite;

            }
        }
        //Debug.Log(fillFraction);
    }
}
