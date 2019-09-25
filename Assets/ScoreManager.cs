using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text progressText;
    private int score;
    private float progress;
    private int numberOfCatches;
    public int numberofCatches { 
        set { numberOfCatches = value; }
    }

    private PerfectCatch[] perfectCatch;

    private void Awake()
    {
        perfectCatch = FindObjectsOfType<PerfectCatch>();
        numberOfCatches = 0;
    }

    public void IncrementScore()
    {
        foreach (var perfect in perfectCatch)
        {
            if (perfect.perfectCatch)
            {
                score += 2;
            }
        }
        score++;
        numberOfCatches++;
        Debug.Log(numberOfCatches);
        if (numberOfCatches == 3)
        {
            progress += 2.5f;
            numberofCatches = 0;
        }
        scoreText.text = "Score: " + score;
        progressText.text = "Progress:" + progress + "%";
    }
    public void ReduceProgress()
    {
        if(progress>=10f)
        {
            progress -= 10f;
        }
        else
        {
            progress = 0.0f;
        }
        progressText.text = "Progress:" + progress + "%";
    }
}
