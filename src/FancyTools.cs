using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

[assembly: ModInfo("testmod",
	Description = "Test Mod",
	Authors     = new []{ "McJty" })]


namespace FancyTools {

    public class FancyTools : ModSystem {

        public static string MODID = "fancytools";

        public Dictionary<string, SeedBagInventory> seedBagInventories = new Dictionary<string, SeedBagInventory>();

        public override void Start(ICoreAPI api) {
            base.Start(api);
			api.RegisterBlockBehaviorClass(TestBehavior.NAME, typeof(TestBehavior));
            api.RegisterItemClass(SeedBag.NAME, typeof(SeedBag));

            // EntityPlayer.TryGiveItemStack();
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
        }
    }
    
}