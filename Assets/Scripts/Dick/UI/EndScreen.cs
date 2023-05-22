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
        updateEndScreenEvent.list += UpdateS;
        gameObject.SetActive(false);
    }

    protected override void OnDestroyScreen() => updateEndScreenEvent.list -= UpdateS;

    protected override void UpdateScreen(int msg)
    {
        gameObject.SetActive(true);
        endScreenMsg.text = $"Your score: {msg}";
    }
}
