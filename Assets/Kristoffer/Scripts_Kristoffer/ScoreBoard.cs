using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private IntEventSO updateScoreBoardEvent;

    private List<Score> scoreList;
    [SerializeField] private ScoreSetter scorePanel;
    [SerializeField] private GameObject submitter;


    private GameObject parent;
    private void Start()
    {       
        scoreList = new List<Score>();
        scoreList = ReaderWriterManager.Instance.GetComponent<ReaderWriterManager>().LoadScore();
        updateScoreBoardEvent.list += CheckScore;

        parent = gameObject.transform.parent.gameObject;
        parent.SetActive(false);
    }

    void OnDestroy() => updateScoreBoardEvent.list -= CheckScore;

    public void CheckScore(int score)
    {
        // Display the scoreboard
        parent.SetActive(true);

        //See if there are less than 10 scores on the list or if the smallest sorted value on the list is less than the new score.
        if (scoreList.Count<10 || scoreList[scoreList.Count - 1].score < score)
        {
            //Activates the submitter allowing you to add the new score to the list.
            submitter.SetActive(true);
            submitter.GetComponent<ScoreSubmitter>().UpdateSubmitter(score);
        }
    }
    
    public void ReplaceScore(int score, string Name)
    {
        //If the number of scores in scorelist are lower than 10 add the new score to the list
        if(scoreList.Count<10)
        {
            Score newscore = new Score(score, Name);
            scoreList.Add(newscore);           
        }
        //The score that have been sorted to the last place will be replaced with the new score and the list re-sorts
        else
        {
            SortList(scoreList);
            scoreList[scoreList.Count-1].score = score;
            scoreList[scoreList.Count-1].Name = Name;
        }
        SortList(scoreList);
    }

    //Uses the MergeSorter in order to sort the ranked scores if there are more than 1 score in scorelist.
    private void SortList(List<Score> scoreList)
    {
        scoreList = MergeSorter.MergeSort(scoreList);
        ReaderWriterManager.Instance.SaveScore(scoreList);
        scorePanel.UpdateScore(scoreList);
    }
}
