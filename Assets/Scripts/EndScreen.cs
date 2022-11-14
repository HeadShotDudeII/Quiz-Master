using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulation blah blah blah  you got a score of " +
                              scoreKeeper.CalculateScore() + "%";
    }


}
