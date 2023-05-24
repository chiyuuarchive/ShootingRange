using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateScoreUIEvent;
    [SerializeField]
    TMP_Text scoreMsg;

    [SerializeField]
    IntEventSO updateAmmoUIEvent;
    [SerializeField]
    TMP_Text ammoMsg;

    [SerializeField]
    IntEventSO updateMagazineUIEvent;
    [SerializeField]
    TMP_Text magazineMsg;

    void Start()
    {
        ammoMsg.text = string.Empty;
        magazineMsg.text = string.Empty;
        scoreMsg.text = "Score: 0";

        updateScoreUIEvent.list += UpdateScoreText;
        updateAmmoUIEvent.list += UpdateAmmoText;
        updateMagazineUIEvent.list += UpdateMagazineText;
    }

    void OnDestroy()
    {
        updateScoreUIEvent.list -= UpdateScoreText;
        updateAmmoUIEvent.list -= UpdateAmmoText;
        updateMagazineUIEvent.list -= UpdateMagazineText;
    }

    void UpdateScoreText(int score) => scoreMsg.text = $"Score: {score}";

    void UpdateAmmoText(int ammo)
    {
        if (ammo >= 0)
            ammoMsg.text = $"Ammo count: {ammo}";
        else
            ammoMsg.text = string.Empty;
    }

    void UpdateMagazineText(int numOfMagazines) => magazineMsg.text = $"Magazines left: {numOfMagazines}";
}
