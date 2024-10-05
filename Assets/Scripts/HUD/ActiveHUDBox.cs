using UnityEngine;

public class ActiveHUBBoc : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();

    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToogleActiveSlot((int)ctx.ReadValue<float>());

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }

    private void ToggleActiveHighLight(int indexNum) {
        activeSlotIndexNum = indexNum;

    foreach (Transform inventorySlot in this.transform)
    {
        inventorySlot.GetChild(0).gameObject.SetActive(false);
    }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
    }

}
