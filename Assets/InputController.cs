using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;
    [SerializeField]
    private float minSwipeDistanceInPercentage = 0.10f;
    private float swipeDistance;
    private bool hasSwipedLeft;
    private bool hasSwipedRight;

    private bool trackMouse;

    public enum SwipeDirection { Up, Down, Left, Right }

    public SwipeDirection swipeDirection { get; private set; }

    private void Start()
    {
        swipeDistance = Screen.height * minSwipeDistanceInPercentage;
    }

    /// <summary>
    /// Update function.
    /// </summary>
    public void Tick()
    {
        HandleInput();
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
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstPosition = touch.position;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
                CheckSwipe();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hasSwipedLeft = false;
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
            firstPosition = Input.mousePosition;
            lastPosition = Input.mousePosition;         
        }

        if (Input.GetMouseButtonUp(0))
        {
            trackMouse = false;
            hasSwipedLeft = false;
            hasSwipedRight = false;
        }

        if (trackMouse)
        {
            lastPosition = Input.mousePosition;
            CheckSwipe();
        }
    }

    /// <summary>
    /// Checks how to player has swiped.
    /// </summary>
    private void CheckSwipe()
    {
        Vector3 direction = lastPosition - firstPosition;

        // check if the player has swiped enough
        if (!(Math.Abs(direction.x) > swipeDistance) &&
            !(Math.Abs(direction.y) > swipeDistance)) return;

        // horizontal swipe
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // swiped to right
            if (lastPosition.x > firstPosition.x)
            {
                swipeDirection = SwipeDirection.Right;
                SwipeLocation();
            }
            // swiped to left
            else
            {
                swipeDirection = SwipeDirection.Left;
                SwipeLocation();
            }
        }
        // vertical swipe
        else
        {
            // swipe up
            if (lastPosition.y > firstPosition.y)
            {
                swipeDirection = SwipeDirection.Up;
                SwipeLocation();
            }
            // swipe down
            else
            {
                swipeDirection = SwipeDirection.Down;
                SwipeLocation();
            }
        }
    }

    /// <summary>
    /// Determines swipe location.
    /// </summary>
    private void SwipeLocation()
    {
        // swiped left side of the screen
        if (firstPosition.x < Screen.width / 2f)
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

    // todo move to another class that handles animation and logic
    private void SwipeLeftScreen()
    {
        Debug.Log("Swiped Left Side");
        switch (swipeDirection)
        {
            case SwipeDirection.Down:
                Debug.Log("Swiped to down");
                // todo play right hand swipe down animation
                break;
            case SwipeDirection.Up:
                Debug.Log("Swiped to up");
                // todo play right hand swipe up animation
                break;
            case SwipeDirection.Right:
                Debug.Log("Swiped to right");
                // todo play right hand swipe to right animation
                break;
            default:
                break;
        }
    }

    // todo move to another class that handles animation and logic
    private void SwipeRightScreen()
    {
        Debug.Log("Swiped Right Side");
        switch (swipeDirection)
        {
            case SwipeDirection.Down:
                Debug.Log("Swiped to down");
                // todo play left hand swipe down animation
                break;
            case SwipeDirection.Up:
                Debug.Log("Swiped to up");
                // todo play left hand swipe up animation
                break;
            case SwipeDirection.Left:
                Debug.Log("Swiped to left");
                // todo play left hand swipe to left side animation
                break;
            default:
                break;
        }
    }
}
