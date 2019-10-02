using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // If any further game objects are used for registering input,
    // put them as child objects under this GameController object.

    private ViolaController ViolaController;
    private MenuController MenuController;
    private SceneController SceneController;
    private TutorialManager TutorialManager;
    private LastTutorialManager LastTutorialManager;

    private readonly Vector3[] firstPosition = new Vector3[2];
    private readonly Vector3[] lastPosition = new Vector3[2];
    [SerializeField]
    private float minSwipeDistanceInPercentage = 0.10f;
    private float verticalSwipeDistance;
    private float horizontalSwipeDistance;
    private bool hasSwipedLeftScreen;
    private bool hasSwipedRightScreen;

    private bool trackMouse;
    private bool _tutorialLevel;
    private bool _lastTutorialLevel;

    public enum SwipeDirection { Up, Down, Left, Right }

    private ViolaController.HandType screenSide;
    private ViolaController.ThrowType throwType;

    public bool InvertControls { get; set; }
    public bool HandCutOff { get; set; }

    private void Awake()
    {
        ViolaController = FindObjectOfType<ViolaController>();
        //MenuController = GetComponent<MenuController>(); // Uncomment if you need it.
        SceneController = FindObjectOfType<SceneController>();
        if (FindObjectOfType<TutorialManager>() != null)
        {
            TutorialManager = FindObjectOfType<TutorialManager>();
            _tutorialLevel = true;
        }
        else
            _tutorialLevel = false;
        if (FindObjectOfType<LastTutorialManager>() != null)
        {
            LastTutorialManager = FindObjectOfType<LastTutorialManager>();
            _lastTutorialLevel = true;
        }
        else
            _lastTutorialLevel = false;
    }

    private void Start()
    {
        verticalSwipeDistance = Screen.height * minSwipeDistanceInPercentage;
        horizontalSwipeDistance = Screen.width * minSwipeDistanceInPercentage;
    }


    /// <summary>
    /// Update function.
    /// </summary>
    public void Update()
    {
        throwType = ViolaController.ThrowType.None;

        HandleInput();

        if (throwType != ViolaController.ThrowType.None)
        {
            if (!_tutorialLevel)
            {
                ViolaController.Throw(throwType, screenSide);
                return;
            }

            if (_tutorialLevel && throwType == ViolaController.ThrowType.HighThrow)
            {
                if (TutorialManager._previousTutorialStage == 0)
                {
                    TutorialManager.RemoveTutorialUI(0);
                    ViolaController.Throw(throwType, screenSide);
                }
                if (TutorialManager._previousTutorialStage == 5)
                {
                    TutorialManager.RemoveTutorialUI(5);
                    ViolaController.Throw(throwType, screenSide);
                }
            }

            if (_tutorialLevel && throwType == ViolaController.ThrowType.FloorBounce && TutorialManager._previousTutorialStage == 1)
            {
                ViolaController.Throw(throwType, screenSide);
                TutorialManager.RemoveTutorialUI(1);
            }

            if (_tutorialLevel && throwType == ViolaController.ThrowType.MidThrow && TutorialManager._previousTutorialStage == 2)
            {
                ViolaController.Throw(throwType, screenSide);
                TutorialManager.RemoveTutorialUI(2);
            }
            if (_tutorialLevel && TutorialManager._previousTutorialStage > 3 && TutorialManager._previousTutorialStage != 5)
            {
                Debug.Log("Throw funcking ball");
                ViolaController.Throw(throwType, screenSide);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
            SceneController.ResetScene();
    }


    /// <summary>
    /// Handles the input.
    /// </summary>
    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PcInput();
#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }


    /// <summary>
    /// Handles mobile input.
    /// </summary>
    private void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            if (_tutorialLevel && TutorialManager._previousTutorialStage!=5)
                TutorialManager.RemoveTutorialUI(3);
            if (_lastTutorialLevel)
                LastTutorialManager.RemoveTutorialUI();
        }

        Touch[] touches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (touches[i].phase == TouchPhase.Began)
            {
                firstPosition[i] = touches[i].position;
                lastPosition[i] = touches[i].position;
            }
            else if (touches[i].phase == TouchPhase.Moved)
            {
                lastPosition[i] = touches[i].position;
                CheckSwipe(i);
            }
            else if (touches[i].phase == TouchPhase.Ended)
            {
                AkSoundEngine.PostEvent("SwipeSound_event", gameObject);
                // swipe was on left side of the screen
                if (firstPosition[i].x < Screen.width / 2f)
                {
                    hasSwipedLeftScreen = false;
                }
                // swipe was on right side of the screen
                else
                    hasSwipedRightScreen = false;
            }
        }
    }


    /// <summary>
    /// Handles pc input.
    /// </summary>
    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_tutorialLevel && TutorialManager._previousTutorialStage != 5)
                TutorialManager.RemoveTutorialUI(3);
            if (_lastTutorialLevel)
                LastTutorialManager.RemoveTutorialUI();
            trackMouse = true;
            firstPosition[0] = Input.mousePosition;
            lastPosition[0] = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            AkSoundEngine.PostEvent("SwipeSound_event", gameObject);
            trackMouse = false;
            hasSwipedLeftScreen = false;
            hasSwipedRightScreen = false;
        }

        if (trackMouse)
        {
            lastPosition[0] = Input.mousePosition;
            CheckSwipe(0);
        }
    }


    /// <summary>
    /// Checks how to player has swiped.
    /// </summary>
    private void CheckSwipe(int i)
    {
        Vector3 directionVector = lastPosition[i] - firstPosition[i];

        if (!SwipedLongEnough(directionVector)) return;

        SwipeDirection direction;

        if (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y))
            direction = HorizontalSwipe(i);
        else
            direction = VerticalSwipe(i);

        screenSide = SwipeLocation(i);
        throwType = ComputeThrowType(direction);
    }


    private bool SwipedLongEnough(Vector3 direction)
    {
        return !(!(Math.Abs(direction.x) > horizontalSwipeDistance) &&
            !(Math.Abs(direction.y) > verticalSwipeDistance));
    }


    private SwipeDirection HorizontalSwipe(int i)
    {
        if (lastPosition[i].x > firstPosition[i].x)
            return SwipeDirection.Right;
        else
            return SwipeDirection.Left;
    }


    private SwipeDirection VerticalSwipe(int i)
    {
        if (lastPosition[i].y > firstPosition[i].y)
            return SwipeDirection.Up;
        else
            return SwipeDirection.Down;
    }


    /// <summary>
    /// Determines swipe location.
    /// </summary>
    private ViolaController.HandType SwipeLocation(int i)
    {
        if (firstPosition[i].x < Screen.width / 2f)
        {
            // normal controls swipe left
            if (!InvertControls)
            {
                if (hasSwipedLeftScreen)
                    return ViolaController.HandType.None;

                hasSwipedLeftScreen = true;
                return ViolaController.HandType.Right;
            }

            // inverted controls
            if (hasSwipedRightScreen)
                return ViolaController.HandType.None;

            hasSwipedRightScreen = true;
            return ViolaController.HandType.Left;
        }

        // ignore the right swipe input when hand is cut off
        if (HandCutOff)
            return ViolaController.HandType.None;

        // normal control swipe right
        if (!InvertControls)
        {
            if (hasSwipedRightScreen)
                return ViolaController.HandType.None;

            hasSwipedRightScreen = true;
            return ViolaController.HandType.Left;
        }

        // inverted controls
        if (hasSwipedLeftScreen)
            return ViolaController.HandType.None;

        hasSwipedLeftScreen = true;
        return ViolaController.HandType.Right;
    }


    private ViolaController.ThrowType ComputeThrowType(SwipeDirection direction)
    {
        if (screenSide == ViolaController.HandType.None)
            return ViolaController.ThrowType.None;

        switch (direction)
        {
            case SwipeDirection.Up:
                return ViolaController.ThrowType.HighThrow;

            case SwipeDirection.Down:
                return ViolaController.ThrowType.FloorBounce;

            default:
                return ViolaController.ThrowType.MidThrow;
        }
    }

}
