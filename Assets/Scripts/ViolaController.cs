using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolaController : MonoBehaviour
{
    // Keeps track of all functionality in regards to Viola.

    [SerializeField]
    private GameObject BallControllerRef;
    [SerializeField]
    private GameObject ScoreControllerRef;

    private BallController BallController;
    private ScoreController ScoreController;

    private ViolaMove NextMove;

    public Hand leftHand;
    public Hand rightHand;

    public enum ViolaMove
    {
        None,
        HighThrowLeft,
        MidThrowLeft,
        FloorBounceLeft,
        HighThrowRight,
        MidThrowRight,
        FloorBounceRight
    }

    // This is called from the InputController. You receive the input here.
    public void Input(ViolaMove move)
    {
        NextMove = move;
    }


    // Start is called before the first frame update
    void Start()
    {
        BallController = BallControllerRef.GetComponent<BallController>();
        ScoreController = ScoreControllerRef.GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Do something. 
        switch (NextMove)
        {
            case ViolaMove.None:
                break;

            case ViolaMove.MidThrowLeft:
                MidThrow();
                break;

            case ViolaMove.HighThrowLeft:
                HighThrow();
                break;

            case ViolaMove.FloorBounceLeft:
                FloorBounce();
                break;
            case ViolaMove.MidThrowRight:
                MidThrow();
                break;

            case ViolaMove.HighThrowRight:
                HighThrow();
                break;

            case ViolaMove.FloorBounceRight:
                FloorBounce();
                break;
        }
    }

    private void MidThrow()
    {
        AudioController.PlaySFX();
    }

    private void HighThrow()
    {
        AudioController.PlaySFX();
    }

    private void FloorBounce()
    {
        AudioController.PlaySFX();
    }
}
