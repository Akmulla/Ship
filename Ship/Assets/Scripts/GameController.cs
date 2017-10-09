using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Game,Dialog};

public class GameController : MonoBehaviour
{
    public static GameController gc;
    float saved_time_scale;
    public GameState State { get; set; }
    int lvl;
    // public DialogData[] dialogData;
    bool delay=false;

    IEnumerator DelayInput()
    {
        delay = true;
        yield return new WaitForSeconds(0.5f);
        delay = false;
    }
    public int Lvl
    {
        get
        {
            return lvl;
        }
        set
        {
            lvl = value;
            ScoreManager.sm.UpdateLvlText(lvl);
        }
    }


    void Awake()
    {
        gc = GetComponent<GameController>();
        saved_time_scale = 1.0f;
        
       
    }

	void Start ()
    {
        // State = GameState.Game;
        State = GameState.Dialog;
        Lvl = 0;
        UIManager.ui.UpdateUI(State);
        Spawn.spawn.SpawnShip();
        
    }
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (State)
        {
            case GameState.Game:

                break;

            case GameState.Dialog:
                if ((!delay)&&(Input.GetKey(KeyCode.Space)))
                {
                    StartCoroutine(DelayInput());
                    if (UIManager.ui.NextLine())
                    {
                        State = GameState.Game;
                        UIManager.ui.UpdateUI(State);
                    }
                }
                
                break;
        }
    }

    public void Pause()
    {
        saved_time_scale = Time.timeScale;
        Time.timeScale = 0.0f;
        //State = GameState.Pause;
        //UIController.ui.UpdateUI();
    }

    public void UnPause()
    {
        Time.timeScale = saved_time_scale;
        //State = GameState.Game;
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
    public void NextLvl()
    {
        Lvl++;
    }
  
}
