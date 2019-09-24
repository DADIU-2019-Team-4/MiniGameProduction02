using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{

    public int maxReachedLevel = 1;
    public int[] levelScores = new int[GlobalVariables.totalNumberOfLevels];

    public void SaveScore(int level, int score)
    {
        if(score> levelScores[level])
        {
            levelScores[level] = score;
            Debug.Log("Beat highscore on level: " + level + " (" + score + ")");
        }
    }

    /*
    public int GetScore(int level)
    {
        return levelScores[level];
    }
    */
}

