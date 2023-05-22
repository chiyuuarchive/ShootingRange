using UnityEngine;
using TMPro;

public class StartScreen : Screen
{
    [SerializeField]
    IntEventSO updateStartScreenEvent;
    [SerializeField]
    TMP_Text startScreenMsg;


    protected override void InitiateScreen() => updateStartScreenEvent.list += UpdateS;

    protected override void OnDestroyScreen() => updateStartScreenEvent.list -= UpdateS;

    protected override void UpdateScreen(int msg)
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        startScreenMsg.text = $"Game starting in...{msg}";
    }
}
