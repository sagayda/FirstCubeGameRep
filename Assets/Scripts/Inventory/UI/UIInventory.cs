using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject mouseSlotPrefab;
    private List<UISlot> uiSlots;
    [SerializeField] private GameObject uiWindow;
    private InventoryWithSlots openedInventory;
    public bool isOpen { get; private set; }
    public MouseSlot mouseSlot;
    public int capacity { get; private set; }

    public void Initialize(int capacity, GameObject gridParent, GameObject slotPrefab)
    {
        CreateUISlots(capacity, gridParent, slotPrefab);
        mouseSlot = new MouseSlot(this, Instantiate(mouseSlotPrefab, gridParent.transform.parent, false).AddComponent<UISlot>());
    }

    private void Update()
    {
        if (mouseSlot.UISlot != null)
        {
            GameObject slotGameObj = mouseSlot.UISlot.gameObject;
            slotGameObj.GetComponent<RectTransform>().localPosition = Input.mousePosition / slotGameObj.GetComponentInParent<Canvas>().scaleFactor - new Vector3(960f, 540f, 0f);
        }
        
        OnInventoryUpdated();
    }

    public void CreateUISlots(int capacity, GameObject inventoryGrid, GameObject slotPrefab)
    {
        this.capacity = capacity;
        uiSlots = new List<UISlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            UISlot slot = Instantiate(slotPrefab, inventoryGrid.GetComponent<Transform>(), false).AddComponent<UISlot>();
            slot.SetUiInventory(this);
            uiSlots.Add(slot);
        }
    }

    public bool TryToOpenInventory(InventoryWithSlots inventoryToOpen)
    {
        if (inventoryToOpen != openedInventory)
        {
            if (inventoryToOpen.capacity == capacity)
            {
                openedInventory = inventoryToOpen;
                for (int i = 0; i < capacity; i++)
                    uiSlots[i].SetSlot(inventoryToOpen.slots[i]);

                return true;
            }
            Debug.Log("InventoryOpeningFailed");
            return false;
        }
        return true;
    }

    public bool IsInventoryOpened(InventoryWithSlots inventory)
    {
        return openedInventory == inventory;
    }

    public void OpenUI()
    {
        uiWindow.SetActive(true);
        isOpen= true;
        OnInventoryUpdated();
    }

    public void CloseUI()
    {
        if (!mouseSlot.Slot.isEmpty)
            if (InventoryHandler.TryToAddToInventory(this, openedInventory, mouseSlot.Slot.item))
                mouseSlot.Slot.Clear();

        uiWindow.SetActive(false);
        isOpen= false;
    }
    public void OnInventoryUpdated()
    {
        if (!isOpen)
            return;

        if (mouseSlot.Slot.isChanged)
        {
            mouseSlot.UISlot.Refresh();
            mouseSlot.Slot.isChanged = false;
        }

        for (int i = 0; i < uiSlots.Count; i++)
            if (uiSlots[i].inventorySlot.isChanged)
                uiSlots[i].Refresh();
    }
}
