using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tutorialStage;
 

    public int _previousTutorialStage;
    private GameObject _currentText;
    private bool _isTutorialStarted;
    // Start is called before the first frame update
    void Start()
    {
        _isTutorialStarted = false;
        _previousTutorialStage = 0;
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    public void RemoveTutorialUI(int action)  //0- HighThrow,1- SideThrow, 2 - DownThrow, 3 - targets 
    {
        if ((_previousTutorialStage == action || action >= 3 && _previousTutorialStage>= action) && _currentText.active ==true)
        {
            Time.timeScale = 1f;
            _currentText.SetActive(false);
            _previousTutorialStage++;
            if (_previousTutorialStage == 4 || _previousTutorialStage == 5 || _previousTutorialStage == 7)
                EnableTutorialUI();
        }
    }

    public void EnableTutorialUI()
    {
        if (_previousTutorialStage >= 8)
            return;
       _currentText = _tutorialStage[_previousTutorialStage];
        Debug.Log("Stage:" + _previousTutorialStage);
       _currentText.SetActive(true);
       Time.timeScale = 0f;
       if(!_isTutorialStarted)
       _isTutorialStarted = true;
    }

    IEnumerator Delay()
    {
         yield return new WaitForSeconds(1f);
    }
}
