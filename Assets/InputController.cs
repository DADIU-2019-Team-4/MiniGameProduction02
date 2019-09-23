using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;
    [SerializeField]
    private float minSwipeDistance = 0.10f;
    private float swipeDistance;
    private bool hasSwiped;

    private bool trackMouse;

    private void Start()
    {
        swipeDistance = Screen.height * minSwipeDistance;
    }

    public void Tick()
    {
        HandleInput();
    }

    private void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        PcInput();
#elif UNITY_ANDROID
        MobileInput();
#endif
    }

    /// <summary>
    /// Handles mobile input
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
                Vector3 direction = lastPosition - firstPosition;
                if (direction.magnitude > swipeDistance && !hasSwiped)
                {
                    // swiped left side of the screen
                    if (firstPosition.x < Screen.width / 2f)
                    {
                        // do something with direction.normalized
                        hasSwiped = true;
                    }
                    // swiped right side of the screen
                    else
                    {
                        // do something with direction.normalized
                        hasSwiped = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hasSwiped = false;
            }
        }
    }

    /// <summary>
    /// Handles pc input
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
            hasSwiped = false;
        }

        if (trackMouse && !hasSwiped)
        {
            lastPosition = Input.mousePosition;
            Vector3 direction = lastPosition - firstPosition;
            if (direction.magnitude > swipeDistance)
            {
                // swiped left side of the screen
                if (firstPosition.x < Screen.width / 2f)
                {
                    Debug.Log("Swiped Left Side");
                    // do something with direction.normalized
                    Debug.Log(direction.normalized);
                    hasSwiped = true;
                }
                // swiped right side of the screen
                else
                {
                    Debug.Log("Swiped Right Side");
                    // do something with direction.normalized
                    Debug.Log(direction.normalized);
                    hasSwiped = true;
                }
            }
        }
    }
}
