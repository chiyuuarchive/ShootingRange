using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSetter : MonoBehaviour
{
    [SerializeField] public GameObject scorePanelPrefab;
    private List<GameObject> scorePanels;

    private void Start()
    {
        scorePanels = new List<GameObject>();
    }
    public void UpdateScore(List<Score>scoreList)
    {
        if (scorePanels.Count < scoreList.Count)
        {
            int dif = scoreList.Count - scorePanels.Count;
            for (int i = 0; i < dif; i++)
            {
                GameObject panel = Instantiate(scorePanelPrefab, gameObject.transform);
                scorePanels.Add(panel);
            }
            
        }
        for (int i = 0; i < scoreList.Count; i++)
        {
            scoreList[i].rank = i + 1;
            scorePanels[i].GetComponent<ScorePanel>().UpdateScorePanel(scoreList[i]);
        }
    }
}
