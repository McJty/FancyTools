using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace FancyTools
{
    public class ItemSlotSeeds : ItemSlotSurvival
    {
        public ItemSlotSeeds(SeedBagInventory inventory) : base(inventory)
        {
        }

        public override bool CanHold(ItemSlot itemstackFromSourceSlot)
        {
            return base.CanHold(itemstackFromSourceSlot) && isSeed(itemstackFromSourceSlot);
        }

        public bool isSeed(ItemSlot sourceSlot)
        {
            return sourceSlot.Itemstack != null && sourceSlot.Itemstack.Item is ItemPlantableSeed;
        }

    }
}