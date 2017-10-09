using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text text;
    public Text sausageText;
    public int startScore;
    int score;
    int sausage=0;
    
    public void AddSausage()
    {
        if ((Ship.ship != null) && (sausage < 5) && (Score >= 10)&&(Ship.ship.State==ShipState.Idle))
        {
            sausage++;
            Score -= 10;
            sausageText.text = sausage.ToString();
        }
    }

    public void ResetSausage()
    {
        sausage = 0;
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
            score = value;
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
