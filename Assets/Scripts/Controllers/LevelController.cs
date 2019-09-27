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
            if(Time.timeScale != 0)
            {
                timeOnCurrentLevelTime += Time.deltaTime * (1 / Time.timeScale);
            }
        }
        
        //show current level
        lvlText.text = "lvl " + currentLevel; 

        //When time is up go to next level
        if (totalTimeToCompleteCurrentLevel - timeOnCurrentLevelTime < 0)
        {
            StartLevel(currentLevel+1);

        }
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(0);
    }

    public void StartLevel(int level)
    {
        currentLevel = level;

        if (currentLevel > maxReachedLevel)
        {
            maxReachedLevel = currentLevel;
        }
        timeOnCurrentLevelTime = 0;
        totalTimeToCompleteCurrentLevel = baseTimeToCompleteLevels[currentLevel];

        saveC.SaveGame();
    }

}
