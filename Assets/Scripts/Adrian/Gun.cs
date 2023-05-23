using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem ShootingSystem;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private LayerMask Mask;

    float timeSinceLastShot;

    public int Capacity => gunData.magSize;
    public int AmmoLeft => gunData.currentAmmo;

    private void Start()
    {
        gunData.currentAmmo = gunData.magSize;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload(int ammo)
    {
        if (!gunData.reloading && gameObject.activeSelf)
        {
            StartCoroutine(Reload(ammo));
        }
    }

    private IEnumerator Reload(int ammo)
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = ammo;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public bool Shoot()
    {
        if (gunData.currentAmmo <= 0 || !CanShoot()) return false;

       
        ShootingSystem.Play();
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance, Mask))
        {
            TrailRenderer trail = Instantiate(BulletTrail, muzzle.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hitInfo));
            Debug.Log(hitInfo.transform.name);
            //Deal damage here

        }

        gunData.currentAmmo--;
        timeSinceLastShot = 0;
        return true;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;
            
            yield return null;
        }
        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance, Color.blue);
    }


}
