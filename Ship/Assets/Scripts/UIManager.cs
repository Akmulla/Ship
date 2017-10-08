using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    public GameObject dialogMenu;
    int curLine;
	
	void Awake ()
    {
        ui = this;
        curLine = 0;

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
                break;
        }
    }

    public void NextLine()
    {
        if (GameController.gc.State != GameState.Dialog)
            return;

        curLine++;
    }
}
