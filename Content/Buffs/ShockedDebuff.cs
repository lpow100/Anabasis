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
            if (!npc.active || npc.life <= 0) return;

            // Optional: also stop rotation/attacks that might be driven by AI movement.
            // But do NOT disable gravity.
            npc.ai[0] = 0f;  // only if your mod/pattern uses ai[0] for movement
            npc.ai[1] = 0f;  // idem

            // Optional: prevent the NPC from running tile-based pathing/moving
            // if your NPC AI respects these flags.
            npc.netUpdate = true;
        }
    }
}