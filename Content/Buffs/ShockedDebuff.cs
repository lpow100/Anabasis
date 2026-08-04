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

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!npc.boss)
            {
                npc.velocity.X *= 0;
                npc.velocity.Y *= 0;
                npc.frameCounter = 0;
            }
            else
            {
                npc.velocity.X *= 0.65f;
                npc.velocity.Y *= 0.65f;
                npc.frameCounter *= 0.65;
            }
        }

    }
}
