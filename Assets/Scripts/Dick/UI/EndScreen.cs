using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateEndScreenEvent;
    [SerializeField]
    TMP_Text endScreenMsg;

    void Start()
    {
        updateEndScreenEvent.list += UpdateStartScreen;
        gameObject.SetActive(false);
    }

    private void OnDestroy() => updateEndScreenEvent.list -= UpdateStartScreen;

    void UpdateStartScreen(int msg)
    {
        gameObject.SetActive(true);
        endScreenMsg.text = $"Your score: {msg}";
    }
}
