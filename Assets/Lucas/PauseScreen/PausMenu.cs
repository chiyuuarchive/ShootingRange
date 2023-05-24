using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausMenu : MonoBehaviour
{
    [SerializeField] private Transform pausMenuTransfrom;
    private bool pauseMenuOpen;
    [SerializeField] private EventSO gamePausedEvent;
    [SerializeField] private EventSO gameResumedEvent;
    [SerializeField] private EventSO gameQuitEvent;

    private void Start()
    {
        gamePausedEvent.list += ShowPauseMenu;
        gameResumedEvent.list += ResumeGame;
        gameQuitEvent.list += QuitGame;
    }

    private void OnDestroy()
    {
        gamePausedEvent.list -= ShowPauseMenu;
        gameResumedEvent.list -= ResumeGame;
        gameQuitEvent.list -= QuitGame;
    }

    private void ShowPauseMenu()
    {
        Debug.Log("Show pausemenu");
        pausMenuTransfrom.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Hide pausemenu");
        pausMenuTransfrom.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
