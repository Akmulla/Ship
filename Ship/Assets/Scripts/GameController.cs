using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Game,Pause,Dialog};

public class GameController : MonoBehaviour
{
    public static GameController gc;
    float saved_time_scale;
    public GameState State { get; set; }
    public int Lvl { get; set; }
	
    void Awake()
    {
        gc = GetComponent<GameController>();
        saved_time_scale = 1.0f;
        Lvl = 0;
    }

	void Start ()
    {
        State = GameState.Game;
        Spawn.spawn.SpawnShip();
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

    public void ShipReached(bool success,int price=0)
    {
        Spawn.spawn.SpawnShip();
        if (success)
        {
            ScoreManager.sm.Score += price;
        }
        
    }

  
}
