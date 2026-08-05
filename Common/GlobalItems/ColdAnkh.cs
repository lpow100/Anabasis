using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Common.GlobalItems
{
    public class ColdAnkh : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.AnkhCharm || item.type == ItemID.AnkhShield)
            {
                player.buffImmune[BuffID.Frozen] = true;
                player.buffImmune[BuffID.Chilled] = true;
            }
        }
    }
}
