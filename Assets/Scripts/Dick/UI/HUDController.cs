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

    void UpdateAmmoText(int ammo) => ammoMsg.text = $"Ammo count: {ammo}";

    void UpdateMagazineText(int numOfMagazines) => magazineMsg.text = $"Magazines left: {numOfMagazines}";
}
