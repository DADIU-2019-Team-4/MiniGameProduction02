﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private InputController InputController;
    private OptionController OptionController;
    private AvatarController AvatarController;
    private ScoreController ScoreController;
    private GraphicController GraphicController;
    private AudioController AudioController;


    // Start is called before the first frame update
    void Start()
    {
        InputController = GetComponent<InputController>();
        OptionController = GetComponent<OptionController>();
        AvatarController = GetComponent<AvatarController>();
        ScoreController = GetComponent<ScoreController>();
        GraphicController = GetComponent<GraphicController>();
        AudioController = GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputController.Tick();
        OptionController.Tick();
        AvatarController.Tick();
        ScoreController.Tick();

        // Maybe we don't need these?
        //GraphicController.Tick();
        //AudioController.Tick();
    }
}
