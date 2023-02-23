using UnityEngine;

public class InterfaceInteraction : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int slotsCount;
    public UIInventory uiInventory { get; private set; }
    public InventoryWithSlots inventoryWithSlots { get; private set; }

    private void Awake()
    {
        inventoryWithSlots = InventoryWithSlots.Initialize(slotsCount);

        uiInventory = inventoryObject.GetComponent<UIInventory>();
        uiInventory.Initialize(inventoryWithSlots.capacity, inventoryObject, slotPrefab);
        uiInventory.TryToOpenInventory(inventoryWithSlots);
    }

    private void Start()
    {
        uiInventory.gameObject.GetComponent<Tester>().SetRandomItems(inventoryWithSlots);
        uiInventory.OnInventoryUpdated();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryWithSlots.isOpen)
                inventoryWithSlots.CloseInventoryInUi(uiInventory);
            else
                inventoryWithSlots.OpenInventoryInUi(uiInventory);
        }
    }
}
