using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateAmmoEvent;
    [SerializeField]
    IntEventSO updateMagazineEvent;

    Weapon currentWeapon;
    List<Magazine> magazines;

    public bool HasWeapon => currentWeapon != null;

    void Start() => magazines = new List<Magazine>();

    public void AddWeapon(Weapon weapon)
    {
        Destroy(currentWeapon);
        Weapon w = Instantiate(weapon);
        w.Mount();
        currentWeapon = w;
        updateAmmoEvent?.Invoke(w.Capacity);
        Debug.Log("Pick up weapon");
    }

    public bool UseWeapon()
    {
        // Check if player is holding a weapon
        if (!HasWeapon) return false;

        if (currentWeapon.Fire())
        {
            updateAmmoEvent?.Invoke(currentWeapon.AmmoLeft);
            return true;
        }

        // Attempt to reload if weapon is out of ammo
        if (currentWeapon.AmmoLeft <= 0)
            if (UseMagazine()) Debug.Log("Weapon has been reloaded");

        return false;
    }
    public void Reload()
    {
        if (!HasWeapon) return;
        UseMagazine();
    }

    public void AddMagazine(Magazine mag)
    {
        magazines.Add(mag);
        updateMagazineEvent?.Invoke(magazines.Count);
        Debug.Log("Pick up magazine");
    }


    bool UseMagazine()
    {
        // If there's no magazine in the inventory don't reload.
        if (magazines.Count <= 0) return false;

        Magazine mag = magazines[0];
        magazines.Remove(mag);

        // Check if ammunition doesn't exceed ammo capacity of the weapon
        int ammo = mag.AmmoCount > currentWeapon.Capacity ? currentWeapon.Capacity : mag.AmmoCount;

        // Reload weapon
        currentWeapon.Reload(ammo);
        updateAmmoEvent?.Invoke(ammo);
        updateMagazineEvent?.Invoke(magazines.Count);
        return true;
    }

   
}