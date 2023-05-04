using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateStartScreenEvent;
    [SerializeField]
    TMP_Text startScreenMsg;

    void Start() => updateStartScreenEvent.list += UpdateStartScreen;
    private void OnDestroy() => updateStartScreenEvent.list -= UpdateStartScreen;

    void UpdateStartScreen(int msg) 
    {
        if (msg == -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        startScreenMsg.text = $"Game starting in...{msg}";
    }
}
