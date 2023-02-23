using System;
public class InventorySlot : IInventorySlot
{
    public IInventory inventory {get; private set;}
    public IInventoryItem item { get; private set; }
    public bool isFull => !isEmpty && Amount == Capacity;
    public bool isEmpty => item == null;
    public string itemId => item.info.id;
    public int Amount
    {
        get 
        {
            return item == null ? 0 : item.state.Amount; 
        }
        set 
        { 
            if(item != null)
            {
                item.state.Amount = value;
                isChanged = true;
            }
        }
    }

    private int capavity;

    public int Capacity
    {
        get
        {
            if (item == null)
                return -1;

            return item.info.maxStackSize;
        }
    }
    public bool isChanged { get; set; }

    public InventorySlot(IInventory inventory)
    {
        this.inventory = inventory;
    }
    public InventorySlot()
    {
        inventory= null;
    }
    public void SetItem(IInventoryItem item)
    {
        if (!isEmpty)
            return;
        this.item = item.Clone();
        isChanged= true;
    }
    public IInventoryItem ReplaceItem(IInventoryItem item, bool returnItem = true) 
    {
        if (returnItem)
        {
            var itemToReturn = this.item.Clone();
            this.item = item.Clone();
            isChanged = true;
            return itemToReturn;
        }
        else
        {
            ReplaceItem(item);
            isChanged= true;
        }

        return null;
    }
    public void ReplaceItem (IInventoryItem item)
    {
        this.item = item.Clone();
        isChanged = true;
    }
    public void Clear()
    {
        if (isEmpty)
            return;
        
        isChanged= true;
        item.state.Amount = 0;
        item = null;
    }
}
