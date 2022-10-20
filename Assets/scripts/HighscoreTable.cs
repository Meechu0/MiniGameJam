using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{

    public Transform entryContainer;
    public Transform entryTemplate;

    public int maxEntryCount;
    public float entryPlacementDifference;

    public Color firstPlaceColour;

    private List<Transform> highscoreEntryTransformList;


    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
    }

    // create entries for highscore table
    public void FillScoreSlots()
    {
        AddHighScoreEntry(500, "meechu");
        AddHighScoreEntry(450, "meechu");
        AddHighScoreEntry(400, "meechu");
        AddHighScoreEntry(300, "meechu");
        AddHighScoreEntry(200, "meechu");
        AddHighScoreEntry(127, "meechu");
        AddHighScoreEntry(125, "meechu");
        AddHighScoreEntry(100, "meechu");
        AddHighScoreEntry(10, "meechu");
        AddHighScoreEntry(1, "meechu");

    }
    // checking if playerPrefs with keyname exists   -- if not then im creating 10 entries with different highscores.
    public void HasKey(string keyname)
    {
        if (PlayerPrefs.HasKey(keyname))
        {
            Debug.Log("The key " + keyname + " exists");
        }
        else
            FillScoreSlots();
    }

    private void Start()
    {
        //checking for playerprefs existance
        HasKey("highScoreTable");

        entryTemplate.gameObject.SetActive(false);

        // converting playerPrefs highscoreTable to json.
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);

        highscoreEntryTransformList = new List<Transform>();

        // 
        //cycle through elements
        //sort based on score
        for (int i = 0; i < highScore.highScoreEntryList.Count; i++)
        {
            for (int j = i; j < highScore.highScoreEntryList.Count; j++)
            {
                if (highScore.highScoreEntryList[j].score > highScore.highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScore.highScoreEntryList[i];
                    highScore.highScoreEntryList[i] = highScore.highScoreEntryList[j];
                    highScore.highScoreEntryList[j] = tmp;

                }
            }
        }

        // limit score entries (change the quantity in inspector)
        if (highScore.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScore.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScore.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        foreach (HighScoreEntry highScoreEntry in highScore.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }


    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -entryPlacementDifference * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;

            case 1:
                rankString = "1ST";
                break;

            case 2:
                rankString = "2ND";
                break;

            case 3:
                rankString = "3RD";
                break;
        }
        entryTransform.Find("Position").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = highscoreEntry.score.ToString();
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highscoreEntry.name;
        // every second entry enables background.
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        // rank 1 color  *inspector)
        if (rank == 1)
        {
            entryTransform.Find("Position").GetComponent<TextMeshProUGUI>().color = firstPlaceColour;
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().color = firstPlaceColour;
            entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().color = firstPlaceColour;

        }
        transformList.Add(entryTransform);
    }

    // passing name and score value to store in highscore table. 
    public void AddHighScoreEntry(int score, string name)
    {
        // create HighScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScoreTmp = JsonUtility.FromJson<HighScores>(jsonString);

        // score entries cap (maxentryCount *inspector*)
        if (highScoreTmp != null && highScoreTmp.highScoreEntryList.Count > maxEntryCount)
        {
            for (int h = highScoreTmp.highScoreEntryList.Count; h > maxEntryCount; h--)
            {
                highScoreTmp.highScoreEntryList.RemoveAt(maxEntryCount);
            }
        }

        // load highscores
        HighScores highScore = JsonUtility.FromJson<HighScores>(jsonString);

        // dds new entry to HighscoreTable if empty
        if (highScore == null)
        {
            var highScoreFirstEntry = new List<HighScoreEntry>() { highScoreEntry };
            highScore = new HighScores { highScoreEntryList = highScoreFirstEntry };
        }
        else
        {
            // Add new entry to highscore list
            highScore.highScoreEntryList.Add(highScoreEntry);
        }


        // save updated list to playerprefs
        string json = JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    // single highscore entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}