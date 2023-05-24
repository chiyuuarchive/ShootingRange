using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    IntEventSO updateScoreUIEvent;
    [SerializeField] 
    IntEventSO updateScoreEvent;
    [SerializeField]
    EventSO displayScoreEvent;

    [SerializeField]
    EventSO displayScoreBoardEvent;
    [SerializeField]
    IntEventSO updateScoreBoardEvent;

    int currentScore = 0;

    void Start()
    {
        updateScoreEvent.list += UpdateScore;
        displayScoreBoardEvent.list += DisplayScoreBoard;
    }
    void OnDestroy()
    {
        updateScoreEvent.list -= UpdateScore;
    }

    // Invoked when player score changes to game events
    void UpdateScore(int score)
    {
        currentScore += score;
        updateScoreUIEvent?.Invoke(currentScore);
    }

    void DisplayScoreBoard() => updateScoreBoardEvent?.Invoke(currentScore);
}
