using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    IntEventSO updateAmmoEvent;
    [SerializeField]
    IntEventSO updateMagazineEvent;

    Gun currentWeapon;
    List<Magazine> magazines;

    public bool HasWeapon => currentWeapon != null;

    void Start() => magazines = new List<Magazine>();

    public void PickWeapon(Gun weapon)
    {
        if (HasWeapon) DropWeapon();

        PickUpController pc = weapon.GetComponent<PickUpController>();
        pc.PickUp();

        currentWeapon = weapon;
        updateAmmoEvent?.Invoke(weapon.AmmoLeft);
        Debug.Log("Pick up weapon");
    }

    public void DropWeapon()
    {
        PickUpController p = currentWeapon.GetComponent<PickUpController>();
        p.Drop();

        currentWeapon = null;
        updateAmmoEvent?.Invoke(-1);
    }

    public void UseWeapon()
    {
        // Check if player is holding a weapon
        if (!HasWeapon) return;

        if (currentWeapon.Shoot())
        {
            updateAmmoEvent?.Invoke(currentWeapon.AmmoLeft);
            return;
        }

        // Attempt to reload if weapon is out of ammo
        if (currentWeapon.AmmoLeft <= 0)
            UseMagazine();
    }
    public void AddMagazine(Magazine mag)
    {
        magazines.Add(mag);
        updateMagazineEvent?.Invoke(magazines.Count);
        Debug.Log("Pick up magazine");
    }

    public void UseMagazine()
    {
        // If there's no magazine in the inventory don't reload.
        if (magazines.Count <= 0) return;

        Magazine mag = magazines[0];
        magazines.Remove(mag);

        // Check if ammunition doesn't exceed ammo capacity of the weapon
        int ammo = mag.AmmoCount > currentWeapon.Capacity ? currentWeapon.Capacity : mag.AmmoCount;

        // Reload weapon
        currentWeapon.StartReload(ammo);
        updateAmmoEvent?.Invoke(ammo);
        updateMagazineEvent?.Invoke(magazines.Count);
    }

   
}