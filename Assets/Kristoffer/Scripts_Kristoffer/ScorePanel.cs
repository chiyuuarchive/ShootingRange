using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI nameText;

    public void UpdateScorePanel(int rank, string score, string Name)
    {
        rankText.text = rank.ToString();
        scoreText.text = score;
        nameText.text = Name;
    }
}
