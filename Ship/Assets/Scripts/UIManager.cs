using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    public GameObject dialogMenu;
    public Text dialogText;
    public DialogData[] dialogData;
    int curLine;
    public GameObject captain;
    public GameObject onion;
    public GameObject gameOverMenu;
    DialogData data;

    void Awake ()
    {
        ui = this;
        curLine = 0;
        DontDestroyOnLoad(gameObject);
    }
	
    public void UpdateUI(GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                dialogMenu.SetActive(false);
                break;
            case GameState.Dialog:
                dialogMenu.SetActive(true);
                curLine = 0;
                data = dialogData[GameController.gc.Lvl];
                UpdateLine();
                break;
            case GameState.GameOver:
                dialogMenu.SetActive(false);
                gameOverMenu.SetActive(true);
                break;
        }
    }
    //returns true if last line
    public bool NextLine()
    {
        if (GameController.gc.State != GameState.Dialog)
            return true;
        curLine++;
        if (curLine == dialogData[GameController.gc.Lvl].lines.Length )
        {
            
            return true;
        }
        else
        {
            UpdateLine();
            return false;
        }
            
    }
    void UpdateLine()
    {
        dialogText.text = data.lines[curLine].line;
        captain.SetActive(data.lines[curLine].captainActive);
        onion.SetActive(data.lines[curLine].onionActive);
    }

   
}
