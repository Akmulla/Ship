using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    [SerializeField]
    public Text text;
    [SerializeField]
    public Text sausageText;
    [SerializeField]
    public Text lvlText;
    [SerializeField]
    public Text weightText;
    public int weight=100;
    public int startScore;
    int score;
    int sausage=0;
    
    public void UpdateLvlText(int lvl)
    {
        //print(lvlText);
        //print(lvl);
        lvlText.text = lvl.ToString();
    }
    public void AddSausage()
    {
        if ((Ship.ship != null) && (sausage < 5) && (Score >= 10)&&(Ship.ship.State==ShipState.Idle))
        {
            SoundManager.sm.SingleSound(SoundSample.Button);
            sausage++;
            Score -= 10;
            weight = (100 + sausage * 10);
            weightText.text = weight.ToString();
            sausageText.text = sausage.ToString();
        }
    }

    public void ResetSausage()
    {
        //if ((sausageText == null) || (weightText == null))
        //    return;
        sausage = 0;
        weight = (100 + sausage * 10);
        weightText.text = weight.ToString();
        sausageText.text = sausage.ToString();
    }
    public int GetSausage()
    {
        return sausage;
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (value<0)
            {
                score = 0;
                GameController.gc.GameOver();
            }
            else
            {
                score = value;
            }
            
            text.text = score.ToString();
        }
    }

    void Awake()
    {
        sm = this;
    }

	void Start()
    {
        Score = startScore;
    }
}
