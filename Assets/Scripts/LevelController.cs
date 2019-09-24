using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int totalNumberOfLevels;
    public float[] baseTimeToCompleteLevels;
    public float totalTimeToCompleteCurrentLevel;
    public int currentLevel=1;
    public float timeOnCurrentLevelTime = 0;
    public bool timerEnabled = true;

    public Text timeText;
    public Text lvlText;

    private void Awake()
    {
        //baseTimeToCompleteLevels = new float[totalNumberOfLevels+1];
    }

    private void Start()
    {
        totalTimeToCompleteCurrentLevel = baseTimeToCompleteLevels[currentLevel];
    }

    // Update is called once per frame
    void Update()
    {
        //update time spent on current level, independant of timescale.
        if (timerEnabled)
        {
            timeOnCurrentLevelTime += Time.deltaTime * (1 / Time.timeScale);
        }
        
        //show time left of level & current level
        timeText.text = (totalTimeToCompleteCurrentLevel - timeOnCurrentLevelTime).ToString("F1") ;
        lvlText.text = "lvl " + currentLevel; 

        //When time is up go to next level
        if (totalTimeToCompleteCurrentLevel - timeOnCurrentLevelTime < 0)
        {
            currentLevel++;
            timeOnCurrentLevelTime = 0;
            totalTimeToCompleteCurrentLevel = baseTimeToCompleteLevels[currentLevel];
        }
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(0);
    }

    
}
