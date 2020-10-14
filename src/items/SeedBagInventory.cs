using System;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace FancyTools
{
    public class SeedBagInventory : InventoryBase
    {

        internal ItemSlot seedBagSlot;

        internal ItemSlot[] slots;

        public SeedBagInventory(string className, string instanceID, ICoreAPI api, ItemSlot seedBagSlot) : base(className, instanceID, api)
        {
            slots = GenEmptySlots(8);
            this.seedBagSlot = seedBagSlot;
        }

        protected override ItemSlot NewSlot(int index)
        {
            return new ItemSlotSeeds(this);
        }

        /**
         * Place this itemstack in the inventory. The stackSize of the parameter will be modified
         * If it is 0 everything is placed.
         */
        public void PlaceItemStack(ItemStack stack)
        {
            // First find a slot that has the same seed and still has room
            foreach (ItemSlot slot in slots)
            {
                if (slot.Itemstack != null)
                {
                    if (slot.Itemstack.Item == stack.Item)
                    {
                        int remaining = slot.Itemstack.Item.MaxStackSize - slot.Itemstack.StackSize;
                        int tomove = Math.Min(remaining, stack.StackSize);
                        if (tomove > 0)
                        {
                            slot.Itemstack.StackSize += tomove;
                            stack.StackSize -= tomove;
                        }
                    }
                    if (stack.StackSize <= 0)
                    {
                        return;
                    }
                }
            }

            // Otherwise find an empty slot
            foreach (ItemSlot slot in slots)
            {
                if (slot.Itemstack == null)
                {
                    slot.Itemstack = stack.Clone();
                    stack.StackSize = 0;
                    return;
                }
            }
        }

        public void SyncToSeedBag()
        {
            if (seedBagSlot.Itemstack != null && seedBagSlot.Itemstack.Item is SeedBagItem)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (!(seedBagSlot.Itemstack is null))
                    {
                        seedBagSlot.Itemstack.Attributes.SetItemstack("s" + i, slots[i].Itemstack);
                    }
                }    
                seedBagSlot.MarkDirty();            
            }
        }

        internal void SyncFromSeedBag()
        {
            if (seedBagSlot.Itemstack != null && seedBagSlot.Itemstack.Item is SeedBagItem)
            {
                for (int i = 0; i < 8; i++)
                {
                    slots[i].Itemstack = seedBagSlot.Itemstack.Attributes.GetItemstack("s" + i);
                }
            }
        }

        public override ItemSlot this[int slotId]
        {
            get => slots[slotId];
            set => slots[slotId] = value;
        }

        public override int Count => slots.Length;

        public override void FromTreeAttributes(ITreeAttribute tree)
        {
            SlotsFromTreeAttributes(tree, slots);

        }

        public override void ToTreeAttributes(ITreeAttribute tree)
        {
            SlotsToTreeAttributes(slots, tree);
        }
    }
}