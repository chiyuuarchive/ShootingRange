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
    float startDelay = 4f;
    [SerializeField]
    float gameDuration = 15f;
    [SerializeField]
    float showMsgAt = 10f;

    float counter;
    bool gameHasStarted, gameHasEnded;

    InputAction restartKeyPressed, quitGameKeyPressed;

    public bool GameHasStarted => gameHasStarted;

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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void RestartKeyButtonPressed()
    {
        Debug.Log("Restart button pressed");

        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
