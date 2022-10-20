using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AddToHighScores : MonoBehaviour
{
    public GameObject HighscorePanel;
    private HighscoreTable _HighscoreTable;
    public int score;
    public TextMeshProUGUI name;

    public void submit()
    {
       // Time.timeScale = 1;
        HighscorePanel.SetActive(true);
        _HighscoreTable = HighscorePanel.GetComponent<HighscoreTable>();
        _HighscoreTable.AddHighScoreEntry(score, name.text);
        SceneManager.LoadScene(0);


    }
}
