using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // If any further game objects are used for registering input,
    // put them as child objects under this GameController object.

    private ViolaController ViolaController;
    private MenuController MenuController;
    private SceneController sceneController;

    private readonly Vector3[] firstPosition = new Vector3[2];
    private readonly Vector3[] lastPosition = new Vector3[2];
    [SerializeField]
    private float minSwipeDistanceInPercentage = 0.10f;
    private float swipeDistance;
    private bool hasSwipedLeft;
    private bool hasSwipedRight;

    private bool trackMouse;

    public enum SwipeDirection { Up, Down, Left, Right }

    public SwipeDirection swipeDirection { get; private set; }

    private void Awake()
    {
        ViolaController = FindObjectOfType<ViolaController>();
        //MenuController = GetComponent<MenuController>(); // Uncomment if you need it.
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        swipeDistance = Screen.height * minSwipeDistanceInPercentage;
    }

    /// <summary>
    /// Update function.
    /// </summary>
    public void Update()
    {
        HandleInput();

        if (Input.GetKeyDown(KeyCode.Space))
            sceneController.SceneReset();
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
                // swipe was on left side of the screen
                if (firstPosition[i].x < Screen.width / 2f)
                    hasSwipedLeft = false;
                // swipe was on right side of the screen
                else
                    hasSwipedRight = false;
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
            trackMouse = true;
            firstPosition[0] = Input.mousePosition;
            lastPosition[0] = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            hasSwipedLeft = false;
            hasSwipedRight = false;
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
        Vector3 direction = lastPosition[i] - firstPosition[i];

        // check if the player has swiped enough
        if (!(Math.Abs(direction.x) > swipeDistance) &&
            !(Math.Abs(direction.y) > swipeDistance)) return;

        // horizontal swipe
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // swiped to right
            if (lastPosition[i].x > firstPosition[i].x)
            {
                swipeDirection = SwipeDirection.Right;
                SwipeLocation(i);
            }
            // swiped to left
            else
            {
                swipeDirection = SwipeDirection.Left;
                SwipeLocation(i);
            }
        }
        // vertical swipe
        else
        {
            // swipe up
            if (lastPosition[i].y > firstPosition[i].y)
            {
                swipeDirection = SwipeDirection.Up;
                SwipeLocation(i);
            }
            // swipe down
            else
            {
                swipeDirection = SwipeDirection.Down;
                SwipeLocation(i);
            }
        }
    }

    /// <summary>
    /// Determines swipe location.
    /// </summary>
    private void SwipeLocation(int i)
    {
        // swiped left side of the screen
        if (firstPosition[i].x < Screen.width / 2f)
        {
            if (hasSwipedLeft) return;

            hasSwipedLeft = true;
            SwipeLeftScreen();
        }
        // swiped right side of the screen
        else
        {
            if (hasSwipedRight) return;

            hasSwipedRight = true;
            SwipeRightScreen();
        }
    }

    private void SwipeLeftScreen()
    {
        Debug.Log("Swiped Left Side");
        switch (swipeDirection)
        {
            case SwipeDirection.Down:
                Debug.Log("Swiped to down");
                ViolaController.Input(ViolaController.ViolaMove.FloorBounceRight);
                //rightHand.Throw("Right", "Down");
                break;
            case SwipeDirection.Up:
                Debug.Log("Swiped to up");
                ViolaController.Input(ViolaController.ViolaMove.HighThrowRight);
                break;
            case SwipeDirection.Right:
                Debug.Log("Swiped to right");
                ViolaController.Input(ViolaController.ViolaMove.MidThrowRight);
                break;
            default:
                ViolaController.Input(ViolaController.ViolaMove.None);
                break;
        }
    }

    private void SwipeRightScreen()
    {
        Debug.Log("Swiped Right Side");
        switch (swipeDirection)
        {
            case SwipeDirection.Down:
                Debug.Log("Swiped to down");
                ViolaController.Input(ViolaController.ViolaMove.FloorBounceLeft);
                break;
            case SwipeDirection.Up:
                Debug.Log("Swiped to up");
                ViolaController.Input(ViolaController.ViolaMove.HighThrowLeft);
                break;
            case SwipeDirection.Left:
                Debug.Log("Swiped to left");
                ViolaController.Input(ViolaController.ViolaMove.MidThrowLeft);
                break;
            default:
                ViolaController.Input(ViolaController.ViolaMove.None);
                break;
        }
    }
}
