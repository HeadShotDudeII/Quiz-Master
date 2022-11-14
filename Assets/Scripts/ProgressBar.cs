using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalQuesion;
    public int questionAnswered;
    public float fillFraction;

    public void setTotalQuestion(int totalQ)
    {
        totalQuesion = totalQ;
    }

    public void setQuestionAnswered(int qAnswered)
    {
        questionAnswered = qAnswered;
    }

    public float CalCulateFillFraction()
    {
        return fillFraction = (float)questionAnswered / (float)totalQuesion;
        //Debug.Log("fillfraction is " + fillFraction);

    }


}
