using System.Collections.Generic;
using Vintagestory.API.Common;

[assembly: ModInfo("fancytools",
    Description = "Fancy Tools",
    Authors = new[] { "McJty" })]


namespace FancyTools
{

    public class FancyTools : ModSystem {

        public static string MODID = "fancytools";

        public Dictionary<string, SeedBagInventory> seedBagInventories = new Dictionary<string, SeedBagInventory>();

        public override void Start(ICoreAPI api) {
            base.Start(api);
			// api.RegisterBlockBehaviorClass(TestBehavior.NAME, typeof(TestBehavior));
            // api.RegisterBlockEntityClass("FancyMold", typeof(BlockEntityFancyMold));

            api.RegisterItemClass(SeedBagItem.NAME, typeof(SeedBagItem));
            api.RegisterItemClass(SpadeItem.NAME, typeof(SpadeItem));
        }
    }
    
}