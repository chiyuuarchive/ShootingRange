using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ReaderWriterManager : MonoBehaviour
{
    public static ReaderWriterManager Instance;
    private string filePath;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        filePath = Path.Combine(Application.persistentDataPath, "ScoreContainer.txt");
    }
    public void SaveScore(List<Score> scores)
    {
        string scoreData = "";
        foreach(Score score in scores)
        {
            scoreData += score.score.ToString() + "," + score.Name + "\n";
        }
        
        File.WriteAllText(filePath, scoreData);
    }
    public List<Score> LoadScore()
    {
        List<Score> scores = new List<Score>();
        if (File.Exists(filePath))
        {
            string scoreData = File.ReadAllText(filePath);
            string[] scoreLines = scoreData.Split('\n');
            
            foreach (string scoreLine in scoreLines)
            {
                string[] scoreParts = scoreLine.Split(',');
                if (scoreParts.Length == 2)
                {                  
                    string name = scoreParts[1];
                    int score;
                    if (scoreParts[0]!=null)
                    {
                        Score scoreDataObj = new Score(Convert.ToInt32(scoreParts[0]), name);
                        scores.Add(scoreDataObj);                       
                    }
                }
            }
        }
        return scores;
    }   
}
