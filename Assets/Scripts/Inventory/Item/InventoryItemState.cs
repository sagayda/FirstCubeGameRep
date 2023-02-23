using System;
namespace Assets.Scripts
{
    [Serializable]
    public class InventoryItemState : IInventoryItemState
    {
        private int amount;
        public int Amount { get => amount; set => amount = value; }

        private bool isEquiped;
        public bool IsEquipped { get => isEquiped; set => isEquiped = value; }

        public InventoryItemState()
        {
            amount = 1;
            isEquiped = false;
        }

        public IInventoryItemState Clone()
        {
            return new InventoryItemState { amount = amount, isEquiped = isEquiped };
        }
    }
}
