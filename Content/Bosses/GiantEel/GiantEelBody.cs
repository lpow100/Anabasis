using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;


namespace Anabasis.Content.Bosses.GiantEel
{
    public class GiantEelBody : ModNPC
    {
        // ai[0] = whoAmI of the segment ahead of this one
        // ai[1] = whoAmI of the head (for realLife lookups / convenience)

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 46;
            NPC.damage = 36;
            NPC.defense = 12;
            NPC.lifeMax = 1; // segments usually shouldn't be killable independently
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1; // irrelevant, we override via realLife life sharing below
        }


        public override void AI()
        {
            int aheadIndex = (int)NPC.ai[0];
            if (aheadIndex < 0 || aheadIndex >= Main.maxNPCs || !Main.npc[aheadIndex].active)
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
                return;
            }

            NPC ahead = Main.npc[aheadIndex];

            // Follow at a fixed distance behind the segment ahead - classic worm chaining
            Vector2 toAhead = ahead.Center - NPC.Center;
            float desiredDistance = 34f;
            float currentDistance = toAhead.Length();

            if (currentDistance > 0f)
            {
                Vector2 dir = toAhead / currentDistance;
                Vector2 targetPos = ahead.Center - dir * desiredDistance;
                NPC.Center = Vector2.Lerp(NPC.Center, targetPos, 0.5f);
                NPC.velocity = dir * ahead.velocity.Length();
                NPC.rotation = dir.ToRotation() + MathHelper.PiOver2;
            }

            // Keep realLife pointed at the head so damage numbers / boss bar attribute correctly
            NPC.realLife = (int)NPC.ai[1];
        }

        public override bool CheckActive() => false;

        public override bool? CanBeHitByItem(Player player, Item item) => NPC.dontTakeDamage ? false : null;
        public override bool? CanBeHitByProjectile(Projectile projectile) => NPC.dontTakeDamage ? false : null;
    }
}
