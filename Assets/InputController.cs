using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // If any further game objects are used for registering input,
    // put them as child objects under this GameController object.

    [SerializeField]
    private GameObject Viola;

    private ViolaController ViolaController;
    //private MenuController MenuController; // Uncomment if you need it.

    // Start is called before the first frame update
    void Start()
    {
        ViolaController = Viola.GetComponent<ViolaController>();
        //MenuController = GetComponent<MenuController>(); // Uncomment if you need it.
    }

    // Update is called once per frame
    void Update()
    {
        ViolaController.ViolaMove nextMove;

        //nextMove = ViolaController.ViolaMove.MidThrow;
        nextMove = ViolaController.ViolaMove.None;

        ViolaController.Input(nextMove); // How to send input to ViolaController.
    }
}
