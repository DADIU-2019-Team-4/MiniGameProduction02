using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{
    // This class is public static to avoid object instantiation.
    // For example, in other scripts, write:   'AudioController.FunctionToCall(parameter)'

    // Why have an AudioController, instead of each object call Wwise?
    // Answer: Visual Studio keeps track of references to all function calls.

    public static void PlaySFX()
    {
        
    }
}
