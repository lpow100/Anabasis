using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Anabasis.Content.Bosses.GiantEel
{
    // Head segment - controls AI, spawns/keeps track of body & tail
    public class GiantEelHead : ModNPC
    {
        // AI slot usage
        private ref float AITimer => ref NPC.ai[0];
        private ref float AttackState => ref NPC.ai[1]; // 0 = swim/chase, 1 = charging lunge, 2 = lunging, 3 = instakill

        private const int NumBodySegments = 12;
        private int[] bodySegmentIndices;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPCID.Sets.TrailCacheLength[Type] = 5;
            NPCID.Sets.TrailingMode[Type] = 0;

            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 68;
            NPC.damage = 40;
            NPC.defense = 14;
            NPC.lifeMax = 6600;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 4, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;   // handled manually via water buoyancy
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;       // fully custom AI, not reusing vanilla styles
            NPC.lavaImmune = false;

            NPC.BossBar = ModContent.GetInstance<GiantEelBossBar>();
        }

        public override void OnSpawn(IEntitySource source)
        {
            bodySegmentIndices = new int[NumBodySegments];
            int previous = NPC.whoAmI;

            for (int i = 0; i < NumBodySegments; i++)
            {
                bool isTail = i == NumBodySegments - 1;
                int type = isTail
                    ? ModContent.NPCType<GiantEelTail>()
                    : ModContent.NPCType<GiantEelBody>();

                int seg = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, type);
                Main.npc[seg].realLife = NPC.whoAmI;
                Main.npc[seg].ai[0] = previous; // link to segment ahead of it
                Main.npc[seg].ai[1] = NPC.whoAmI; // always know the head
                Main.npc[seg].netUpdate = true;

                bodySegmentIndices[i] = seg;
                previous = seg;
            }
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];

            // Retarget if needed
            if (NPC.target < 0 || NPC.target == 255 || target.dead || !target.active)
            {
                NPC.TargetClosest();
                target = Main.player[NPC.target];
            }

            bool inWater = target.wet;

            if (!inWater)
            {
                AttackState = 3;
            }

            Vector2 toTarget = target.Center - NPC.Center;
            float distance = toTarget.Length();

            if ((int)AttackState == 4)
            {
                NPC.damage = 99999;
            } else
            {
                NPC.damage = 40;
            }

            switch ((int)AttackState)
            {
                case 0: // normal swim/chase
                    SwimChase(target);

                    AITimer++;
                    if (AITimer > 240 && distance < 500f) // every ~4s, try a lunge if close enough
                    {
                        AttackState = 1;
                        AITimer = 0;
                        NPC.velocity *= 0.3f; // brief pause telegraphs the charge
                        NPC.netUpdate = true;
                    }
                    break;

                case 1: // charging - short windup, stay mostly still, face target
                    AITimer++;
                    NPC.velocity *= 0.9f;

                    if (AITimer > 30)
                    {
                        AttackState = 2;
                        AITimer = 0;
                        Vector2 dir = toTarget.SafeNormalize(Vector2.UnitX);
                        NPC.velocity = dir * 16f; // lunge speed
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;
                    }
                    break;

                case 2: // lunging
                    AITimer++;
                    if (AITimer > 20)
                    {
                        AttackState = 0;
                        AITimer = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                case 3: // charging - short windup, stay mostly still, face target
                    AITimer++;
                    NPC.velocity *= 0.9f;

                    if (AITimer > 5)
                    {
                        AttackState = 4;
                        AITimer = 0;
                        NPC.velocity = toTarget * 1.5f; // lunge speed
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;
                    }
                    break;

                case 4: // lunging
                    AITimer++;
                    if (AITimer > 20)
                    {
                        AttackState = 0;
                        AITimer = 0;
                        NPC.netUpdate = true;
                    }
                    break;
            }

            // Rotation follows velocity so the head visually points the way it swims
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;

            DespawnIfPlayerFarOrDead(target);
        }

        private void SwimChase(Player target)
        {
            Vector2 toTarget = target.Center - NPC.Center;
            float distance = toTarget.Length();
            Vector2 dir = toTarget.SafeNormalize(Vector2.UnitY);

            // Base swim speed, with a gentle sine wobble perpendicular to travel direction
            // for a more "swimming" feel than a straight worm-line chase.
            float baseSpeed = MathHelper.Lerp(4f, 8f, MathHelper.Clamp(distance / 800f, 0f, 1f));
            Vector2 perpendicular = new Vector2(-dir.Y, dir.X);
            float wobble = (float)System.Math.Sin(Main.GameUpdateCount * 0.05f) * 1.5f;

            Vector2 desiredVelocity = dir * baseSpeed + perpendicular * wobble;
            NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVelocity, 0.05f);
        }

        private void SwimTowardWater()
        {
            // Simple fallback: just apply gravity-ish pull and let it slide back into
            // the ocean's slope. For a real build you'd raycast toward nearest water tile.
            NPC.velocity.Y += 0.3f;
            if (NPC.velocity.Y > 8f) NPC.velocity.Y = 8f;
        }

        private void DespawnIfPlayerFarOrDead(Player target)
        {
            if (!target.active || target.dead || Vector2.Distance(NPC.Center, target.Center) > 4000f)
            {
                NPC.velocity.Y -= 0.2f;
                NPC.EncourageDespawn(10);
            }
        }

        public override bool CheckActive() => false; // bosses shouldn't vanish off-screen

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, hit.HitDirection, -1f);
            }
        }

        public override void OnKill()
        {
            // Kill all connected segments when the head dies
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active && (other.type == ModContent.NPCType<GiantEelBody>()
                                   || other.type == ModContent.NPCType<GiantEelTail>()))
                {
                    if (other.realLife == NPC.whoAmI)
                        other.life = 0;
                }
            }
        }
    }
}
