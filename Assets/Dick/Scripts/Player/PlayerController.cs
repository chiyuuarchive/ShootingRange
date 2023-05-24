using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float pickUpDistance;
    [SerializeField]
    EventSO gameEndedEvent;
    [SerializeField]
    EventSO gamePausedEvent;
    [SerializeField]
    EventSO gameResumedEvent;

    InputAction shootAction, pickUpAction, reloadAction, dropWeaponAction, pauseGameAction;
    bool isLMBPressed;
    bool isInputEnabled;

    Inventory inventory;

    void Awake()
    {
        shootAction = new InputAction(binding: "<Mouse>/leftButton");
        shootAction.started += ctx => isLMBPressed = true;
        shootAction.canceled += ctx => isLMBPressed = false;

        pickUpAction = new InputAction(binding: "<Keyboard>/f");
        pickUpAction.performed += ctx => PickUpButtonPressed();

        reloadAction = new InputAction(binding: "<Mouse>/rightButton");
        reloadAction.performed += ctx => ReloadButtonPressed();

        dropWeaponAction = new InputAction(binding: "<Keyboard>/e");
        dropWeaponAction.performed += ctx => DropWeapon();


        gameEndedEvent.list += DisableInput;
        gamePausedEvent.list += DisableInput;
        gameResumedEvent.list += EnableInputs;

        inventory = GetComponent<Inventory>();
    }

    private void OnDestroy()
    {
        gameEndedEvent.list -= DisableInput;
        gamePausedEvent.list -= DisableInput;
        gameResumedEvent.list -= EnableInputs;
    }

    private void Start()
    {
        EnableInputs();
    }

    private void OnEnable()
    {
        shootAction?.Enable();
        pickUpAction?.Enable();
        reloadAction?.Enable();
        dropWeaponAction?.Enable();
        pauseGameAction?.Enable();
    }

    private void OnDisable()
    {
        shootAction?.Disable();
        pickUpAction?.Disable();
        reloadAction?.Disable();
        dropWeaponAction?.Disable();
        pauseGameAction?.Disable();
    }

    void Update()
    {
        if (isInputEnabled == false) return;
        if (isLMBPressed) inventory.UseWeapon();
    }

    void ReloadButtonPressed()
    {
        if (isInputEnabled == false) return;
        if (inventory.HasWeapon) inventory.UseMagazine();
    }

    void PickUpButtonPressed()
    {
        if (isInputEnabled == false) return;
        RaycastForInteraction();
    }

    void DropWeapon()
    {
        if (isInputEnabled == false) return;
        if (inventory.HasWeapon) inventory.DropWeapon();
    }

    void DisableInput()
    {
        isInputEnabled = false;
    }

    void EnableInputs()
    {
        isInputEnabled = true;
    }



    #region Pickup input handler
    void RaycastForInteraction()
    {
        Transform t = Camera.main.transform;
        Ray r = new Ray(t.position, t.forward);
        Vector3 targetPos = t.position + (t.forward * pickUpDistance);

        if (Physics.Raycast(r, out RaycastHit hit))
        {
            // Make sure to add collider + tag with "Weapon" to weapon objects
            if (hit.collider.gameObject.CompareTag("Weapon"))
                InvokePickUpWeapon(hit);

            if (hit.collider.gameObject.CompareTag("Magazine"))
                InvokePickUpMagazine(hit);

            Debug.DrawLine(t.position, hit.point, Color.red, 2f);
        }
        else
            Debug.DrawLine(t.position, targetPos, Color.yellow, 2f);
    }

    void InvokePickUpWeapon(RaycastHit hit)
    {
        // Check if player is close enough to take the weapon
        if ((hit.collider.gameObject.transform.position - transform.position).magnitude > pickUpDistance) return;

        Gun wep = hit.collider.GetComponentInParent<Gun>();
        if (wep != null) inventory.PickWeapon(wep);
    }

    void InvokePickUpMagazine(RaycastHit hit)
    {
        // Check if player is close enough to take the magazine
        if ((hit.collider.gameObject.transform.position - transform.position).magnitude > pickUpDistance) return;

        Magazine magazine = hit.collider.GetComponent<Magazine>();

        if (magazine != null)
        {
            inventory.AddMagazine(magazine);
            Destroy(magazine.gameObject);
        }
    }
    #endregion
}
