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
        updateAmmoUIEvent.list += UpdateAmmo;
        updateMagazineUIEvent.list += UpdateMagazine;
    }

    void OnDestroy()
    {
        updateScoreUIEvent.list -= UpdateScoreText;
        updateAmmoUIEvent.list -= UpdateAmmo;
        updateMagazineUIEvent.list -= UpdateMagazine;
    }

    void UpdateScoreText(int score) => scoreMsg.text = $"Score: {score}";

    void UpdateAmmo(int ammo) => ammoMsg.text = $"Ammo count: {ammo}";

    void UpdateMagazine(int numOfMagazines) => magazineMsg.text = $"Magazines left: {numOfMagazines}";
}
