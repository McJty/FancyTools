using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace FancyTools
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SyncSeedBagContentsMessage
    {
        public byte[][] stacks;
    }

    public class TestModNetworkSystem : ModSystem
    {
        #region Server
        IServerNetworkChannel serverChannel;
        ICoreServerAPI serverApi;

        public override void StartServerSide(ICoreServerAPI api)
        {
            serverApi = api;

            serverChannel =
                api.Network.RegisterChannel("networkapitest")
                .RegisterMessageType(typeof(SyncSeedBagContentsMessage))
                .SetMessageHandler<SyncSeedBagContentsMessage>(OnClientMessage)
            ;
        }

        private void OnClientMessage(IPlayer fromPlayer, SyncSeedBagContentsMessage message)
        {
            // fromPlayer.InventoryManager.ActiveHotbarSlot.Itemstack.Attributes.GetItemstack;
        }

        #endregion


    }

}