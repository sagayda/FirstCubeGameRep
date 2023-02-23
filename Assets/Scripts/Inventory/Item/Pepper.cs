using Assets.Scripts.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory
{
    public class Pepper : IInventoryItem
    {
        public Type type => GetType();

        public IInventoryItemInfo info { get; }

        public IInventoryItemState state { get; }

        public Pepper(IInventoryItemInfo info)
        {
            this.info = info;
            state = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clonedPepper = new Pepper(info);
            clonedPepper.state.Amount = state.Amount;
            return clonedPepper;
        }
    }
}
