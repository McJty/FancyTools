using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace FancyTools
{

    public class GuiSeedBag : GuiDialog
    {

        public override string ToggleKeyCombinationCode => null;


        private SeedBagInventory inventory;
        private ItemSlot slotGrid;

        public GuiSeedBag(ICoreClientAPI api, SeedBagInventory inventory, ItemSlot slot) : base(api)
        {
            this.inventory = inventory;
            this.slotGrid = slot;

            SetupDialog();
            this.OnClosed += CloseMe;
        }

        private void CloseMe()
        {
            object packet = inventory.Close(capi.World.Player);
            capi.Network.SendPacketClient(packet);
        }

        private void SetupDialog()
        {
            // Auto-sized dialog at the center of the screen
            ElementBounds dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);
            ElementBounds invBounds = ElementBounds.Fixed(0, 40, 250, 120);

            // Background boundaries. Again, just make it fit it's child elements, then add the text as a child element
            ElementBounds bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
            bgBounds.BothSizing = ElementSizing.FitToChildren;
            bgBounds.WithChildren(invBounds);

            // Lastly, create the dialog
            SingleComposer = capi.Gui.CreateCompo("SeedBagDialog", dialogBounds)
                .AddDialogTitleBar("Seed Bag", OnTitleBarClose)
                .AddShadedDialogBG(bgBounds)
                .AddItemSlotGrid(inventory, SendInvPacket, 4, invBounds)
                .Compose();
        }

        public override void OnBeforeRenderFrame3D(float deltaTime)
        {
            base.OnBeforeRenderFrame3D(deltaTime);
            if (slotGrid.Itemstack is null || !(slotGrid.Itemstack.Item is SeedBagItem))
            {
                TryClose();
            }
        }

        private void SendInvPacket(object packet)
        {
            capi.Network.SendPacketClient(packet);
        }

        private void OnTitleBarClose()
        {
            TryClose();
        }

    }

}