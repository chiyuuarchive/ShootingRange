using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmManager : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateMagazineUIEvent;
    [SerializeField]
    IntEventSO updateMagazineEvent;     // For picking up weapon and reloading

    [SerializeField]
    IntEventSO updateAmmoUIEvent;
    [SerializeField]
    IntEventSO updateAmmoEvent;

    int currentAmmo = 0;
    int magazineCount = 0;

    private void Start()
    {
        updateMagazineEvent.list += UpdateMagazineEvent;
        updateAmmoEvent.list += UpdateAmmo;
    }

    private void OnDestroy()
    {
        updateMagazineEvent.list -= UpdateMagazineEvent;
        updateAmmoEvent.list -= UpdateAmmo;
    }

    void UpdateMagazineEvent(int count)
    {
        magazineCount = count;
        updateMagazineUIEvent?.Invoke(magazineCount);
    }

    void UpdateAmmo(int ammo)
    {
        currentAmmo = ammo;
        updateAmmoUIEvent?.Invoke(currentAmmo);
    }
}
