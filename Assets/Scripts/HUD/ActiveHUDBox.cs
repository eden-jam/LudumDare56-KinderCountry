using UnityEngine;

public class ActiveHUDBoc : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();

    }

    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
        ToggleActiveAbilitie(numValue);
    }


    private void ToggleActiveHighLight(int indexNum) {
        activeSlotIndexNum = indexNum;

    foreach (Transform inventorySlot in this.transform)
    {
        inventorySlot.GetChild(0).gameObject.SetActive(false);
    }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
    }

    public void ToggleActiveAbilitie(int indexNum)
    {
        switch (indexNum)
        {
            case 1: ClickDetector.Instance.SetState(State.Move);
                break;
            case 2: ClickDetector.Instance.SetState(State.SpawnFlee);
                break;
            case 3: ClickDetector.Instance.SetState(State.Lure);
                break;
            case 4: ClickDetector.Instance.SetState(State.Cry);
                break;
        }

    }
}
