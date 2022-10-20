using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text ScoreLabel;
    public GameObject player;
    public Transform startPosition;
    public float score;
    public GameObject gameOverPanel;
    public AddToHighScores addScore;
    public float enemyScore;

    // Update is called once per frame
    void Update()
    {
        CalculateScore();
    }

    void CalculateScore()
    {
        float dist = player.transform.position.x - startPosition.transform.position.x;
        if (score < dist +enemyScore)
        {
            score = dist +enemyScore;
        }
        ScoreLabel.text = "Score = " + Mathf.Floor(score).ToString();
    }
    public void GameOver()
    {
        print("gameover");
        Time.timeScale=0;
        gameOverPanel.SetActive(true);
        addScore.score = (int)score;
    }
}
