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