using UnityEngine;

// NOTE PLACEHOLDER CLASS!
public class Weapon : MonoBehaviour
{
    [SerializeField]
    int ammoCapacity = 10;
    [SerializeField]
    float fireDelay = 2f;

    int numOfAmmoLeft;
    float timer = 0f;
    bool canShoot, isMounted;

    public int Capacity => ammoCapacity;
    public int AmmoLeft => numOfAmmoLeft;

    private void Start()
    {
        numOfAmmoLeft = ammoCapacity;
        timer = fireDelay;
        canShoot = true;
    }

    private void FixedUpdate()
    {
        if (!canShoot)
            UpdateTimer(Time.fixedDeltaTime);
    }

    private void Update()
    {
        // Align weapon direction with camera
        if (isMounted)
            transform.forward = -(Camera.main.transform.forward);
    }

    void UpdateTimer(float dt)
    {
        timer -= dt;
        if (timer <= 0)
        {
            canShoot = true;
            timer = fireDelay;
        }
    }

    public void Reload(int ammo) => numOfAmmoLeft = ammo;

    public void Mount()
    {
        isMounted = true;
        Transform playerWeaponSocket = GameObject.Find("WeaponSocket").transform;
        transform.parent = playerWeaponSocket;

        // Adjust weapon transform
        transform.position = playerWeaponSocket.position;
        transform.rotation = playerWeaponSocket.rotation;

        // Disable box collider
        GetComponent<BoxCollider>().enabled = false;
    }

    public bool Fire()
    {
        // Check if gun has any ammunition left and is able to shoot
        if (numOfAmmoLeft <= 0 || !canShoot) return false;
        
        numOfAmmoLeft--;
        // Perform raycast and update player score if a target is hit
        RaycastForTargetHit();
        canShoot=false;
        return true;
    }

    #region FIre handlers - Specific for each weapon types
    void RaycastForTargetHit()
    {
        Transform t = Camera.main.transform;
        Ray r = new Ray(t.position, t.forward);
        Vector3 targetPos = t.position + (t.forward * 10);

        if (Physics.Raycast(r, out RaycastHit hit))
        {
            // Make sure to add collider + tag with "Target" to target gameobjects!
            if (hit.collider.gameObject.tag != "Target") return;

            InvokeTargetHit(hit);
            Debug.DrawLine(t.position, hit.point, Color.red, 2f);
        }
        else
            Debug.DrawLine(t.position, targetPos, Color.yellow, 2f);
    }

    void InvokeTargetHit(RaycastHit hit)
    {
        Debug.Log("Update player score!");
        Target ps = hit.collider.GetComponent<Target>();
        ps?.IsHitByPlayer();
    }
    #endregion
}
