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
    IntEventSO updateEndScreenEvent;

    int currentScore = 0;

    void Start()
    {
        updateScoreEvent.list += UpdateScore;
        displayScoreEvent.list += DisplayScore;
    }
    void OnDestroy()
    {
        updateScoreEvent.list -= UpdateScore;
        displayScoreEvent.list -= DisplayScore;
    }

    // Invoked when player score changes to game events
    void UpdateScore(int score)
    {
        currentScore += score;
        updateScoreUIEvent?.Invoke(currentScore);
    }

    void DisplayScore() => updateEndScreenEvent?.Invoke(currentScore);
}
