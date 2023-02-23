using Assets.Scripts.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public class UIInventoryTester
    {
        private IInventoryItemInfo appleInfo;
        private IInventoryItemInfo pepperInfo;
        private UIInventorySlot_old[] uiInventorySlots;

        public InventoryWithSlots inventory { get; }

        public UIInventoryTester(IInventoryItemInfo appleInfo, IInventoryItemInfo pepperInfo, UIInventorySlot_old[] uiInventorySlots)
        {
            this.appleInfo = appleInfo;
            this.pepperInfo = pepperInfo;
            this.uiInventorySlots = uiInventorySlots;

            //inventory = new InventoryWithSlots(20);
        }

        public void FillSlots()
        {
            var allSlots = InventoryHandler.GetAllSlots(this, inventory);
            var availableSlots = new List<IInventorySlot>(allSlots);

            var filledSlots = 6;
            for (int i = 0; i < filledSlots; i++)
            {
                var filledSlot = AddRandomApplesIntoRandomSlot(availableSlots);
                availableSlots.Remove(filledSlot);

                filledSlot = AddRandomPeppersIntoRandomSlot(availableSlots);
                availableSlots.Remove(filledSlot);
            }

            SetupInventoryUI(inventory);
        }

        private IInventorySlot AddRandomApplesIntoRandomSlot(List<IInventorySlot> slots)
        {
            var rSlotIndex = Random.Range(0, slots.Count);
            var rSlot = slots[rSlotIndex];
            var rCount = Random.Range(1, 15);
            var apple = new Apple(appleInfo);
            apple.state.Amount = rCount;
            InventoryHandler.TryToAddToSlot(this, rSlot, apple);
            return rSlot;
        }

        private IInventorySlot AddRandomPeppersIntoRandomSlot(List<IInventorySlot> slots)
        {
            var rSlotIndex = Random.Range(0, slots.Count);
            var rSlot = slots[rSlotIndex];
            var rCount = Random.Range(1, 15);
            var pepper = new Pepper(pepperInfo);
            pepper.state.Amount = rCount;
            InventoryHandler.TryToAddToSlot(this, rSlot, pepper);
            return rSlot;
        }

        private void SetupInventoryUI(InventoryWithSlots inventory)
        {
            var allSlots = InventoryHandler.GetAllSlots(this, inventory);
            var allSlotsCount = allSlots.Length;
            for (int i = 0; i < allSlotsCount; i++)
            {
                var slot = allSlots[i];
                var uiSlot = uiInventorySlots[i];
                uiSlot.SetSlot(slot);
                uiSlot.Refresh();
            }
        }

        private void OnInventoryUpdated(object sender)
        {
            foreach (var uiSlot in uiInventorySlots)
            {
                uiSlot.Refresh();
            }
        }
    }
}
