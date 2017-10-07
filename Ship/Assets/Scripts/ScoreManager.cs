using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text text;
    public int startScore;
    int score;

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
