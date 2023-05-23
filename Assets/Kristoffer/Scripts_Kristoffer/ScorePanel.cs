using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI nameText;

    public void UpdateScorePanel(Score score)
    {
        rankText.text = score.rank.ToString();
        scoreText.text = score.score.ToString();
        nameText.text = score.Name;
    }
}
