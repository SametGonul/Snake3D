using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : ScriptableObject {

    public Text scoreText;
    private int score = 0;
	// Use this for initialization
	void Start () {
        scoreText.text = "Score: " + score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void increaseScore(int foodPoint)
    {
        score += foodPoint;
        scoreText.text = "Score: " + score;
    }

    public void setScoreToZero()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
