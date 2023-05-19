using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSetter : MonoBehaviour
{
    [SerializeField] public GameObject scorePanelPrefab;
    List<GameObject> scorePanels;

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
            scorePanels[i].GetComponent<ScorePanel>().UpdateScorePanel(i + 1, scoreList[i].score.ToString(), scoreList[i].Name);
        }
    }
}
