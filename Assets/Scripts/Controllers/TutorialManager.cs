using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tutorialStage;
 

    private int _previousTutorialStage;
    private GameObject _currentText;
    private bool _isTutorialStarted;
    // Start is called before the first frame update
    void Start()
    {
        _isTutorialStarted = false;
        _previousTutorialStage = 0;
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (!_isTutorialStarted)
            EnableTutorialUI();
    }

    // Update is called once per frame
    public void RemoveTutorialUI(int action)  //0- HighThrow,1- SideThrow, 2 - DownThrow, 3 - targets 
    {
        if (_previousTutorialStage == action)
        {
            Time.timeScale = 1f;
            _currentText.SetActive(false);
            _previousTutorialStage++;
        }

    }

    public void EnableTutorialUI()
    {

            _currentText = _tutorialStage[_previousTutorialStage];
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
