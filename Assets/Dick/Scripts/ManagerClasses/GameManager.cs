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
    EventSO displayScoreBoardEvent;
    [SerializeField]
    EventSO updateTargetImmunityEvent;
    [SerializeField]
    EventSO gameStoppedEvent;
    [SerializeField]
    EventSO gamePausedEvent;
    [SerializeField]
    EventSO gameRestartEvent;
    [SerializeField]
    EventSO gameResumedEvent;


    [SerializeField]
    float startDelay = 4f;
    [SerializeField]
    float gameDuration = 15f;
    [SerializeField]
    float showMsgAt = 10f;

    float counter;
    bool gameHasStarted, gameHasEnded, gameIsPaused;

    InputAction restartKeyPressed, quitGameKeyPressed;

    public bool GameHasStarted => gameHasStarted;

    void ResetTimer() => counter = 0;

    private void Awake()
    {
        restartKeyPressed = new InputAction(binding: "<Keyboard>/tab");
        restartKeyPressed.performed += ctx => RestartKeyButtonPressed();

        quitGameKeyPressed = new InputAction(binding: "<Keyboard>/escape");
        quitGameKeyPressed.performed += ctx => DeterminePause();

        gameResumedEvent.list += ResumeGame;
        gameRestartEvent.list+= RestartGame;
    }

    private void OnDestroy()
    {
        gameResumedEvent.list -= ResumeGame;
        gameRestartEvent.list -= RestartGame;
    }

    void Start() 
    {
        ResetTimer();
        gameHasStarted = false;
        gameHasEnded = false;
        gameIsPaused = false;
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

    private void FixedUpdate()
    {
        if (gameHasEnded || gameIsPaused) return;
        counter += Time.fixedDeltaTime;

        if (!gameHasStarted)
        {
            updateStartScreenEvent?.Invoke((int)(startDelay - counter));
            if (counter >= startDelay)
            {
                gameHasStarted = true;
                updateStartScreenEvent?.Invoke(-1);
                updateTargetImmunityEvent?.Invoke();
                ResetTimer();
            }
            return;
        }

        if (counter >= showMsgAt)
            InvokeWarningMsg();

        if (counter >= gameDuration)
            StopGame();
    }

    void InvokeWarningMsg()
    {
        int secsLeft = (int)(gameDuration - counter);
        updateGameScreenEvent?.Invoke(secsLeft);
    }

    void StopGame()
    {
        gameHasEnded = true;
        Debug.Log("Game has ended!");
        displayScoreBoardEvent?.Invoke();
        updateGameScreenEvent?.Invoke(-1);
        gameStoppedEvent?.Invoke();
        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void DeterminePause()
    {
        if (gameHasEnded) return;

        if (gameIsPaused)
        {
            gameResumedEvent?.Invoke();
            //ResumeGame();
        }
        else
        {
            gamePausedEvent?.Invoke();
            PauseGame();
        }
    }
    void PauseGame()
    {
        gameIsPaused= true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        updateStartScreenEvent?.Invoke(-1);
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        gameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (gameHasStarted)
            updateStartScreenEvent?.Invoke(-1);
        else
            updateStartScreenEvent?.Invoke((int)(startDelay - counter));

        Time.timeScale = 1;
    }


    void RestartKeyButtonPressed()
    {
        Debug.Log("Restart button pressed");
        RestartGame();
    }
    void RestartGame()
    {
        gameResumedEvent?.Invoke(); //set everything to be normal before reset / causes problems otherwise
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
