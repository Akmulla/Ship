﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Game,Pause};

public class GameController : MonoBehaviour
{
    public static GameController gc;
    float saved_time_scale;
    public GameState State { get; set; }
	
    void Awake()
    {
        gc = GetComponent<GameController>();
        saved_time_scale = 1.0f;
    }

	void Start ()
    {
        State = GameState.Game;

    }
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Pause()
    {
        saved_time_scale = Time.timeScale;
        Time.timeScale = 0.0f;
        State = GameState.Pause;
        //UIController.ui.UpdateUI();
    }

    public void UnPause()
    {
        Time.timeScale = saved_time_scale;
        State = GameState.Game;
        //UIController.ui.UpdateUI();
    }
}
