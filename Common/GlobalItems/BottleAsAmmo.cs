using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Common.GlobalItems
{
    public class BottleAsAmmo : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Bottle)
            {
                item.ammo = ItemID.Bottle; // custom ammo type = its own item ID
                item.consumable = true;
            }
        }
    }
}
