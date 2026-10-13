using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Buffs
{
    public class MartianFire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.pvpBuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 34;
        }
    }
}
