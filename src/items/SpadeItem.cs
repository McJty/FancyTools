using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace FancyTools
{

    public class SpadeItem : Item
    {

        public static string NAME { get; } = "fancytools.spade";

        public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
        {
            if (blockSel != null && byEntity.World is IServerWorldAccessor)
            {
                ITreeAttribute treeAttributes = slot.Itemstack.Attributes.GetTreeAttribute("farmland");
                if (treeAttributes == null)
                {
                    PickupFarmland(slot, byEntity, blockSel);
                }
                else
                {
                    PlaceFarmland(slot, byEntity, blockSel, treeAttributes);
                }
            }
            handling = EnumHandHandling.Handled;
        }

        private void PlaceFarmland(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, ITreeAttribute treeAttributes)
        {
            int id = slot.Itemstack.Attributes.GetInt("farmId");
            BlockPos pos = blockSel.Position.AddCopy(blockSel.Face);
            byEntity.World.BlockAccessor.SetBlock(id, pos);
            BlockEntity blockEntity = byEntity.World.BlockAccessor.GetBlockEntity(pos);
            if (blockEntity is BlockEntityFarmland)
            {
                blockEntity.FromTreeAtributes(treeAttributes, byEntity.World);
                blockEntity.MarkDirty();
            }
            slot.Itemstack.Attributes.RemoveAttribute("farmland");
            slot.Itemstack.Attributes.RemoveAttribute("farmId");
        }

        private void PickupFarmland(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel)
        {
            Block block = byEntity.World.BlockAccessor.GetBlock(blockSel.Position);
            if (block is BlockFarmland)
            {
                BlockEntity blockEntity = byEntity.World.BlockAccessor.GetBlockEntity(blockSel.Position);
                if (blockEntity is BlockEntityFarmland)
                {
                    TreeAttribute tree = new TreeAttribute();
                    blockEntity.ToTreeAttributes(tree);
                    slot.Itemstack.Attributes["farmland"] = tree;
                    slot.Itemstack.Attributes.SetInt("farmId", byEntity.World.BlockAccessor.GetBlock(blockSel.Position).Id);
                    byEntity.World.BlockAccessor.SetBlock(0, blockSel.Position);
                    BlockPos cropPos = blockSel.Position.AddCopy(0, 1, 0);
                    Block seedBlock = byEntity.World.BlockAccessor.GetBlock(cropPos);
                    if (seedBlock is BlockCrop)
                    {
                        byEntity.World.BlockAccessor.BreakBlock(cropPos, byEntity as IPlayer);
                    }
                    slot.MarkDirty();
                }
            }
        }

        public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot slot)
        {
            ITreeAttribute treeAttributes = slot.Itemstack.Attributes.GetTreeAttribute("farmland");
            if (treeAttributes == null)
            {
                return new WorldInteraction[]
                {
                    new WorldInteraction
                    {
                        ActionLangCode = "fancytools:heldhelp-pickupfarm",
                        MouseButton = EnumMouseButton.Right
                    }
                };
            }
            else
            {
                return new WorldInteraction[]
                {
                    new WorldInteraction
                    {
                        ActionLangCode = "fancytools:heldhelp-placefarm",
                        MouseButton = EnumMouseButton.Right
                    }
                };
            }
        }
    }
}