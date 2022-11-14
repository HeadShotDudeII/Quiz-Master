using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int totalQuestion = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }


    public void SetTotalQuestion(int total)
    {
        totalQuestion = total;
    }

    public int GetTotalQuestion()
    {
        return totalQuestion;
    }


    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)totalQuestion * 100);
    }
}



