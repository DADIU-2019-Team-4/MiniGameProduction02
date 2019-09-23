using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;
    [SerializeField]
    private float minSwipeDistance = 0.10f;
    private float swipeDistance;

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
                if (direction.magnitude > swipeDistance)
                {
                    Debug.Log(direction.normalized);
                }
            }
        }
    }

    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trackMouse = true;
            firstPosition = Input.mousePosition;
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            trackMouse = false;

        if (trackMouse)
        {
            lastPosition = Input.mousePosition;
            Vector3 direction = lastPosition - firstPosition;
            if (direction.magnitude > swipeDistance)
            {
                Debug.Log(direction.normalized);
            }
        }
    }

}
