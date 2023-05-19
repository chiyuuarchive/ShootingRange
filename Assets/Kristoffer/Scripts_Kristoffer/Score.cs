using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public string Name;
    public int rank;
    public Score(int setScore, string setName)
    {
        this.score = setScore;
        this.Name = setName;
    }
}
