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
        updateGameScreenEvent.list += UpdateScreen;
        gameObject.SetActive(false);
    }

    protected override void OnDestroyScreen() => updateGameScreenEvent.list -= UpdateScreen;

    protected override void OnUpdateScreen(int msg)
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        gameScreenMsg.text = $"Game finishes in...{msg}";
    }
}
