using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace FancyTools
{

    public class SpadeItem : Item
    {

        public static string NAME { get; } = "fancytools.Spade";

        public override bool OnBlockBrokenWith(IWorldAccessor world, Entity byEntity, ItemSlot itemslot, BlockSelection blockSel)
        {
            
            return base.OnBlockBrokenWith(world, byEntity, itemslot, blockSel);
        }

    }
}