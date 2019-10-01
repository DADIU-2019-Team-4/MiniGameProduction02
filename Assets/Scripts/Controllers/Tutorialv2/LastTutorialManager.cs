using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTutorialManager : MonoBehaviour
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
    }

    void Update()
    {
        if (!_isTutorialStarted)
            EnableTutorialUI();
    }

    // Update is called once per frame
    public void RemoveTutorialUI()  
    {
        if (_currentText.active ==true)
        {
            if(_previousTutorialStage !=6)
                Time.timeScale = 1f;
            _currentText.SetActive(false);
            _previousTutorialStage++;
            if (_previousTutorialStage == 1 || _previousTutorialStage == 3 || _previousTutorialStage == 5 || _previousTutorialStage == 6)
                EnableTutorialUI();
        }
    }

    public void EnableTutorialUI()
    {
        if (_previousTutorialStage >= 7)
            return;
       _currentText = _tutorialStage[_previousTutorialStage];
        Debug.Log("Stage:" + _previousTutorialStage);
       _currentText.SetActive(true);
       Time.timeScale = 0f;
       if(!_isTutorialStarted)
       _isTutorialStarted = true;
    }
}
