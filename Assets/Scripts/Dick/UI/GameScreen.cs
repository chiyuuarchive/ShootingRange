using UnityEngine;
using TMPro;

public class GameScreen : Screen
{
    [SerializeField]
    IntEventSO updateGameScreenEvent;
    [SerializeField]
    TMP_Text gameScreenMsg;

    protected override void InitiateScreen()
    {
        updateGameScreenEvent.list += UpdateS;
        gameObject.SetActive(false);
    }

    protected override void OnDestroyScreen() => updateGameScreenEvent.list -= UpdateS;

    protected override void UpdateScreen(int msg)
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        gameScreenMsg.text = $"Game finishes in...{msg}";
    }
}
