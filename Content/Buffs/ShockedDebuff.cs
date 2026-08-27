using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Anabasis.Content.Buffs
{
    public class ShockedDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;

            // This makes the buff show as a debuff and helps with networking/stacking behavior.
            // (Exact flags depend on what you want.)
        }
    }
}
