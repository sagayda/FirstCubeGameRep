using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public interface IInventoryItemInfo
    {
        int maxStackSize { get; }
        string id { get; }
        string title { get; }
        string description { get; }
        Sprite spriteIcon { get; }
    }
}
