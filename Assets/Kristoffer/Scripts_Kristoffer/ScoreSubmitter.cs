using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreSubmitter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] ScoreBoard scoreBoard;

    public void UpdateSubmitter(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SubmitScore()
    {
        //Button "Submit" uses this in order to send the replaceorder back to the scoreboard.
        if(nameText.text!="")
        {
            scoreBoard.ReplaceScore(Convert.ToInt32(scoreText.text), nameText.text);
            scoreText.text = "";
            nameText.text = "";
            gameObject.SetActive(false);
        }        
    }
}
