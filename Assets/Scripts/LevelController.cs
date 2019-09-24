using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public int[] currentLevelScores = new int[GlobalVariables.totalNumberOfLevels];
    public float[] baseTimeToCompleteLevels = new float[GlobalVariables.totalNumberOfLevels];
    public float totalTimeToCompleteCurrentLevel;
    public int currentLevel=1;
    public int maxReachedLevel;
    public float timeOnCurrentLevelTime = 0;
    public bool timerEnabled = true;

    public Text timeText;
    public Text lvlText;
    private SaveController saveC;

    private void Awake()
    {
        //baseTimeToCompleteLevels = new float[totalNumberOfLevels+1];
        saveC = FindObjectOfType<SaveController>();
    }

    private void Start()
    {
        totalTimeToCompleteCurrentLevel = baseTimeToCompleteLevels[currentLevel];
        maxReachedLevel = saveC.save.maxReachedLevel;
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
            if (currentLevel > maxReachedLevel)
            {
                maxReachedLevel = currentLevel;
            }
            timeOnCurrentLevelTime = 0;
            totalTimeToCompleteCurrentLevel = baseTimeToCompleteLevels[currentLevel];

            saveC.SaveGame();
        }
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(0);
    }

}
