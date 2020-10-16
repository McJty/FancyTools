using Vintagestory.API.Common;
using HarmonyLib;
using Vintagestory.GameContent;

namespace FancyTools
{

    [HarmonyPatch(typeof(EntityPlayer), "TryGiveItemStack")]
    class EntityPlayerPatch
    {
        static bool Prefix(EntityPlayer __instance, ItemStack itemstack, ref bool __result)
        {
            if (itemstack.Item is ItemPlantableSeed)
            {
                // Check if there is a seedbag in the hotbar with enough space
                foreach (ItemSlot slot in __instance.Player.InventoryManager.GetHotbarInventory())
                {
                    if (slot != null && slot.Itemstack != null && slot.Itemstack.Item is SeedBagItem)
                    {
                        SeedBagInventory inventory = SeedBagItem.CreateInventory(__instance.Api, slot);
                        inventory.PlaceItemStack(itemstack);
                        inventory.SyncToSeedBag();
                        if (itemstack.StackSize <= 0)
                        {
                            break;
                        }
                    }
                }
            }
            return true;
        }
    }

}