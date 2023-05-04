using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateStartScreenEvent;
    [SerializeField]
    IntEventSO updateGameScreenEvent;
    [SerializeField]
    EventSO displayScoreEvent;

    [SerializeField]
    float startDelay = 4f;
    [SerializeField]
    float gameDuration = 15f;
    [SerializeField]
    float showMsgAt = 10f;

    float counter;
    bool gameHasStarted, gameHasEnded;

    InputAction restartKeyPressed, quitGameKeyPressed;

    void ResetTimer() => counter = 0;

    private void Awake()
    {
        restartKeyPressed = new InputAction(binding: "<Keyboard>/tab");
        restartKeyPressed.performed += ctx => RestartKeyButtonPressed();

        quitGameKeyPressed = new InputAction(binding: "<Keyboard>/escape");
        quitGameKeyPressed.performed += ctx => Application.Quit();
    }

    void Start() 
    {
        ResetTimer();
        gameHasStarted = false;
        gameHasEnded = false;
    }

    private void OnEnable()
    {
        restartKeyPressed?.Enable();
        quitGameKeyPressed?.Enable();
    }

    private void OnDisable()
    {
        restartKeyPressed?.Disable();
        quitGameKeyPressed?.Disable();
    }

    // Use coroutine!! (WIP)
    private void FixedUpdate()
    {
        if (gameHasEnded) return;
        counter += Time.fixedDeltaTime;

        if (!gameHasStarted)
        {
            updateStartScreenEvent?.Invoke((int)(startDelay - counter));
            if (counter >= startDelay)
            {
                gameHasStarted = true;
                updateStartScreenEvent?.Invoke(-1);
                ResetTimer();
            }
            return;
        }

        if (counter >= showMsgAt)
            InvokeWarningMsg();

        if (counter >= gameDuration)
            StopGame();
    }

    // Use coroutine!! (WIP)
    void InvokeWarningMsg()
    {
        int secsLeft = (int)(gameDuration - counter);
        updateGameScreenEvent?.Invoke(secsLeft);
    }

    void StopGame()
    {
        gameHasEnded = true;
        Debug.Log("Game has ended!");
        displayScoreEvent?.Invoke();
        updateGameScreenEvent?.Invoke(-1);
    }

    void RestartKeyButtonPressed()
    {
        Debug.Log("Restart button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
