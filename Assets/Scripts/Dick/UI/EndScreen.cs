using UnityEngine;
using TMPro;

public class EndScreen : Screen
{
    [SerializeField]
    IntEventSO updateEndScreenEvent;
    [SerializeField]
    TMP_Text endScreenMsg;

    protected override void InitiateScreen()
    {
        updateEndScreenEvent.list += UpdateScreen;
        gameObject.SetActive(false);
    }

    protected override void OnDestroyScreen() => updateEndScreenEvent.list -= UpdateScreen;

    protected override void OnUpdateScreen(int msg)
    {
        gameObject.SetActive(true);
        endScreenMsg.text = $"Your score: {msg}";
    }
}
