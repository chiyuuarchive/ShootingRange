using System.Collections;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] float dropForwardForce, dropUpwardForce;

    Rigidbody rb;
    BoxCollider c;


    bool isMounted;
    float timeSinceLastShot;

    public int Capacity => weaponData.magSize;
    public int AmmoLeft => weaponData.currentAmmo;

    private void OnDisable() => weaponData.reloading = false;

    private bool CanShoot() => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<BoxCollider>();
    }

    public void StartReload(int ammo)
    {
        if (!weaponData.reloading && gameObject.activeSelf)
        {
            StartCoroutine(Reload(ammo));
        }
    }

    private IEnumerator Reload(int ammo)
    {
        weaponData.reloading = true;
        yield return new WaitForSeconds(weaponData.reloadTime);
        weaponData.currentAmmo = ammo;
        weaponData.reloading = false;
    }

    public bool Shoot()
    {
        if (weaponData.currentAmmo <= 0 || !CanShoot()) return false;

        Transform t = Camera.main.transform;
        Ray r = new Ray(t.position, t.forward);
        Vector3 targetPos = t.position + (t.forward * weaponData.maxDistance);

        if (Physics.Raycast(r, out RaycastHit hit))
        {
            RaycastForTargetHit(hit);
            Debug.DrawLine(t.position, hit.point, Color.red, 2f);
        }

        Debug.DrawLine(t.position, targetPos, Color.yellow, 2f);
        return true;
    }

    void RaycastForTargetHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag != "Target") return;
        if (hit.transform.TryGetComponent<HitHandler>(out HitHandler hitHandler))
        {
            hitHandler.GetHit();
            weaponData.currentAmmo--;
            timeSinceLastShot = 0;
        }
    }

    public void Mount()
    {
        Transform playerWeaponSocket = GameObject.Find("WeaponSocket").transform;
        transform.parent = playerWeaponSocket;

        rb.isKinematic = true;
        c.isTrigger = true;
    }

    public void Drop()
    {
        Vector3 v = gameObject.GetComponentInParent<Rigidbody>().velocity;

        //Set parent to null
        transform.SetParent(null);

        //Make rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        c.isTrigger = false;

        //Gun carries moment of player
        rb.velocity = v;

        //AddForce
        rb.AddForce(Camera.main.transform.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Camera.main.transform.up * dropUpwardForce, ForceMode.Impulse);


        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
    }
}
