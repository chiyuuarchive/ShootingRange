using UnityEngine;

// NOTE PLACEHOLDER CLASS!
public class Target : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateScoreEvent;
    [SerializeField]
    int score;

    public void IsHitByPlayer()
    {
        updateScoreEvent?.Invoke(score);
        Destroy(gameObject);
    }
}
