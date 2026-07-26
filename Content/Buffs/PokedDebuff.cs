using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Buffs
{
    // This class serves as an example of a debuff that causes constant loss of life
    // See ExampleLifeRegenDebuffPlayer.UpdateBadLifeRegen at the end of the file for more information
    public class PokedDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
            Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
        }

        // Allows you to make this buff give certain effects to the given player
        public override void Update(NPC target, ref int buffIndex)
        {
            target.lifeRegen -= 3;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 3;
        }
    }
}
