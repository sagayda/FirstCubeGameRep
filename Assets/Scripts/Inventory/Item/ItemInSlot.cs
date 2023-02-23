using Assets.Scripts;
using Assets.Scripts.Abstract;
public class ItemInSlot : IInventoryItem
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public string id => info.id;

    public ItemInSlot(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }

    public ItemInSlot(IInventoryItemInfo info, IInventoryItemState state)
    {
        this.info = info;
        this.state = state.Clone();
    }

    public IInventoryItem Clone()
    {
        ItemInSlot clonnedItem = new ItemInSlot(info, state);
        return clonnedItem;
    }

}

