using UnityEngine;
using TMPro;

public class StartScreen : Screen
{
    [SerializeField]
    IntEventSO updateStartScreenEvent;
    [SerializeField]
    EventSO gamePauseEvent;
    [SerializeField]
    EventSO gameResumeEvent;
    [SerializeField]
    TMP_Text startScreenMsg;

    private void Awake()
    {

    }
    protected override void InitiateScreen()
    {
        updateStartScreenEvent.list += UpdateScreen;
        gamePauseEvent.list += HideScreen;
        gameResumeEvent.list += ShowScreen;
    }

    protected override void OnDestroyScreen()
    {
        updateStartScreenEvent.list -= UpdateScreen;
        gamePauseEvent.list -= HideScreen;
        gameResumeEvent.list -= ShowScreen;
    }

    protected override void OnUpdateScreen(int msg)
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        startScreenMsg.text = $"Game starting in...{msg}";
    }

    private void HideScreen()
    {
        gameObject.SetActive(false);
    }

    private void ShowScreen()
    {
        gameObject.SetActive(true);
    }
}
