using Terraria;
using Terraria.ModLoader;
using Anabasis.Common.DamageClasses;
using Anabasis.Common.Players;

namespace Anabasis.Common.GlobalItems
{
    public class AlchemistSaveGlobalItem : GlobalItem
    {
        public override bool ConsumeItem(Item item, Player player) {
            if (item.DamageType == ModContent.GetInstance<AlchemistDamageClass>()) {
                var modPlayer = player.GetModPlayer<AlchemistPlayer>();
                if (Main.rand.NextFloat() < modPlayer.consumableSaveChance) {
                    return false; // roll succeeded — item is NOT consumed
                }
            }
            return true; // consume normally
        }
    }
}