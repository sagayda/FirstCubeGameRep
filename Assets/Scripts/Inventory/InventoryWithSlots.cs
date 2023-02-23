using Assets.Scripts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryWithSlots : IInventory
{
    public int capacity { get; }
    public bool isOpen { get; private set; }
    public bool isFull => slots.All(slot => slot.isFull);
    public List<IInventorySlot> slots;

    private InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;

        slots = new List<IInventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            slots.Add(new InventorySlot(this));
        }
    }
    public static InventoryWithSlots Initialize(int capacity)
    {
        return new InventoryWithSlots(capacity);
    }
    public void OpenInventoryInUi(UIInventory uiInventory)
    {
        if (uiInventory.TryToOpenInventory(this))
        {
            uiInventory.OpenUI();
            isOpen = true;
        }
    }
    public void CloseInventoryInUi(UIInventory uiInventory)
    {
        if (uiInventory.IsInventoryOpened(this))
        {
            uiInventory.CloseUI();
            isOpen = false;
        }
    }
}