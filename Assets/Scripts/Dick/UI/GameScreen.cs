using UnityEngine;
using TMPro;

public class GameScreen : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateGameScreenEvent;
    [SerializeField]
    TMP_Text gameScreenMsg;

    void Start()
    {
        updateGameScreenEvent.list += UpdateGameScreen;
        gameObject.SetActive(false);
    }
    void OnDestroy() => updateGameScreenEvent.list -= UpdateGameScreen;

    void UpdateGameScreen(int msg)
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        gameScreenMsg.text = $"Game finishes in...{msg}";
    }
}
