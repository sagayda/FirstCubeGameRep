using System;
using System.Collections.Generic;

static class InventoryHandler
{
    /// <summary>
    /// Returns true only if item fited in slot
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    /// <returns></returns>

    public static IInventorySlot[] FindAllSlots(object sender, InventoryWithSlots inventory, Predicate<IInventorySlot> match, bool isReversed = false)
    {
        List<IInventorySlot> foundSlots = new();

        for (int i = 0; i < inventory.slots.Count; i++)
            if (match(inventory.slots[i]))
                foundSlots.Add(inventory.slots[i]);

        if (isReversed)
            foundSlots.Reverse();

        return foundSlots.ToArray();
    }
    public static IInventorySlot[] FindAllSlots(object sender, InventoryWithSlots inventory, string id, bool isReversed = false)
    {
        List<IInventorySlot> foundSlots = new();

        for (int i = 0; i < inventory.slots.Count; i++)
            if (!inventory.slots[i].isEmpty && inventory.slots[i].itemId == id)
                foundSlots.Add(inventory.slots[i]);

        if (isReversed)
            foundSlots.Reverse();

        return foundSlots.ToArray();
    }
    public static IInventorySlot FindSlot(object sender, InventoryWithSlots inventory, Predicate<IInventorySlot> match, bool isReversed = false)
    {
        if (isReversed)
        {
            for (int i = inventory.slots.Count - 1; i >= 0; i--)
                if (match(inventory.slots[i]))
                    return inventory.slots[i];
        }
        else
        {
            for (int i = 0; i < inventory.slots.Count; i++)
                if (match(inventory.slots[i]))
                    return inventory.slots[i];
        }
        return null;
    }
    public static IInventorySlot[] GetAllSlots(object sender, InventoryWithSlots inventory)
    {
        return inventory.slots.ToArray();
    }
    public static bool TryToAddToInventory(object sender, InventoryWithSlots inventory, IInventoryItem itemToAdd)
    {
        if (itemToAdd == null)
            return false;

        IInventorySlot[] slotsWithSameItem = FindAllSlots(sender, inventory, itemToAdd.info.id);
        for (int i = 0; i < slotsWithSameItem?.Length; i++)
            if (TryToAddToSlot(sender, slotsWithSameItem[i], itemToAdd))
                return true;

        if (TryToAddToSlot(sender, FindSlot(sender, inventory, slot => slot.isEmpty), itemToAdd))
            return true;

        return false;
    }
    public static bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        if (item == null || slot == null)
            return false;

        if (slot.isEmpty)
        {
            slot.SetItem(item);
            return true;
        }

        if (slot.itemId != item.info.id)
            return false;

        int totalItemAmount = slot.Amount + item.state.Amount;
        bool isFits = totalItemAmount <= slot.Capacity;

        if (isFits)
        {
            slot.Amount = totalItemAmount;
            item = null;
            return true;
        }

        int amountCanAdd = slot.Capacity - slot.Amount;
        slot.Amount += amountCanAdd;
        item.state.Amount -= amountCanAdd;
        return false;
    }
    public static void RemoveItem(object sender, InventoryWithSlots inventory, string id, int amount = 0)
    {
        if (inventory == null)
            return;

        int amountToRemove = amount;
        bool removeAll = false;

        if (amountToRemove <= 0)
            removeAll = true;

        var slots = FindAllSlots(sender, inventory, slot => slot.itemId == id, true);

        if (slots == null || slots.Length == 0)
            return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (removeAll)
            {
                slots[i].Clear();
                continue;
            }

            bool isEnough = slots[i].Amount - amountToRemove >= 0;

            if (isEnough)
            {
                slots[i].Amount -= amountToRemove;
                if (slots[i].Amount <= 0)
                    slots[i].Clear();
                return;
            }

            amountToRemove -= slots[i].Amount;
            slots[i].Clear();
        }
        return;
    }
    public static void RemoveItem(object sender, InventoryWithSlots inventory, IInventoryItem item)
    {
        if (item == null || item.state.Amount == 0)
            return;

        if (inventory == null)
            return;

        RemoveItem(sender, inventory, item.info.id, item.state.Amount);
    }
    public static IInventoryItem[] GetAllItems(object sender, InventoryWithSlots inventory, bool isReversed = false)
    {
        if (inventory == null)
            return null;

        List<IInventoryItem> items = new List<IInventoryItem>();

        for (int i = 0; i < inventory.slots.Count; i++)
            if (!inventory.slots[i].isEmpty)
                items.Add(inventory.slots[i].item);

        if (isReversed)
            items.Reverse();

        return items.ToArray();
    }
    public static IInventoryItem[] FindAllItems(object sender, InventoryWithSlots inventory, Predicate<IInventoryItem> match, bool isReversed = false)
    {
        if (inventory == null)
            return null;

        var items = GetAllItems(sender, inventory);
        List<IInventoryItem> itemsToReturn = new List<IInventoryItem>();

        for (int i = 0; i < items.Length; i++)
            if (match(items[i]))
                itemsToReturn.Add(items[i]);

        if (isReversed)
            itemsToReturn.Reverse();

        return itemsToReturn.ToArray();
    }
    public static IInventoryItem FindItem(object sender, InventoryWithSlots inventory, Predicate<IInventoryItem> match, bool isReversed = false)
    {
        if (inventory == null)
            return null;

        IInventoryItem[] items = GetAllItems(sender, inventory);

        if (isReversed)
        {
            for (int i = items.Length - 1; i >= 0; i--)
                if (match(items[i]))
                    return items[i];
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
                if (match(items[i]))
                    return items[i];
        }

        return null;
    }
    public static IInventoryItem FindItem(object sender, InventoryWithSlots inventory, string id, bool isReversed = false)
    {
        if (inventory == null)
            return null;

        if (isReversed)
        {
            for (int i = 0; i < inventory.slots.Count; i++)
                if (inventory.slots[i].itemId == id)
                    return inventory.slots[i].item;
        }
        else
        {
            for (int i = inventory.slots.Count - 1; i >= 0; i--)
                if (inventory.slots[i].itemId == id)
                    return inventory.slots[i].item;
        }

        return null;
    }
    public static int GetItemsAmount(object sender, InventoryWithSlots inventory, string id)
    {
        if (inventory == null)
            return 0;

        int itemsAmount = 0;
        for (int i = 0; i < inventory.slots.Count; i++)
            if (inventory.slots[i].itemId == id)
                itemsAmount += inventory.slots.Count;

        return itemsAmount;
    }
    public static bool HasItem(object sender, InventoryWithSlots inventory, string id)
    {
        return FindItem(sender, inventory, id) != null;
    }
    public static void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot == toSlot)
            return;

        if (fromSlot.isEmpty)
            return;

        if (toSlot.isFull)
            return;

        if (!toSlot.isEmpty && fromSlot.itemId != toSlot.itemId)
        {
            IInventoryItem item = toSlot.ReplaceItem(fromSlot.item, true);
            fromSlot.ReplaceItem(item);
            return;
        }

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();
            return;
        }

        int slotCapacity = fromSlot.Capacity;
        bool fits = fromSlot.Amount + toSlot.Amount <= slotCapacity;
        int amountToAdd = fits ? fromSlot.Amount : slotCapacity - toSlot.Amount;
        int amountLeft = fromSlot.Amount - amountToAdd;

        toSlot.Amount += amountToAdd;

        if (fits)
            fromSlot.Clear();
        else
            fromSlot.Amount = amountLeft;
    }
}

