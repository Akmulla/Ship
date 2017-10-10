using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Game,Dialog,GameOver};

public class GameController : MonoBehaviour
{
    public static GameController gc;
    float saved_time_scale;
    public GameState State { get; set; }
    int lvl;
    // public DialogData[] dialogData;
    bool delay=false;

    public void GameOver()
    {
        State = GameState.GameOver;
        UIManager.ui.UpdateUI(State);
    }
    public void ReloadGame()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
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
        //if (gc != null)
        //    Destroy(gc.gameObject);
        
           gc = GetComponent<GameController>();
        saved_time_scale = 1.0f;
        DontDestroyOnLoad(gameObject);
        
       
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
                ///////////////
                if (Input.GetKeyDown(KeyCode.End))
                {
                   // NextLvl();
                }
                /////////////////
                break;

            case GameState.Dialog:
                if ((!delay)&&(Input.GetKeyDown(KeyCode.Space)))
                {
                    //StartCoroutine(DelayInput());
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
        
        if (success)
        {
            ScoreManager.sm.Score += price;
            NextLvl();
        }
        else
        {
            ScoreManager.sm.Score -= 200;
            Spawn.spawn.SpawnShip();
        }
        
    }
    public void NextLvl()
    {
        Lvl++;
        Edges.edges.CalcEdges();
        StartCoroutine(changeLvl());
    }
  IEnumerator changeLvl()
    {
        AsyncOperation load= SceneManager.LoadSceneAsync("Lvl_" + Lvl.ToString());
        while (!load.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        State = GameState.Dialog;
        UIManager.ui.UpdateUI(State);
        Spawn.spawn.SpawnShip();
    }
}
