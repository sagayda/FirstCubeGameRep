using Assets.Scripts.Abstract;
using System;
public interface IInventoryItem
{
    IInventoryItemInfo info { get; }
    IInventoryItemState state { get; }
    IInventoryItem Clone();
}
