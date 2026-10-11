using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Bosses.PhobosAndDeimos
{
    public abstract class BaseMartianMoon : ModNPC
    {
        protected abstract int PartnerType { get; }
        protected abstract bool IsCharger { get; }
        public int partner = -1;

        public override void SetDefaults()
        {
            NPC.width = 58;
            NPC.height = 62;
            NPC.scale = 1.75f;
            NPC.damage = 350;
            NPC.defense = 18;
            NPC.lifeMax = 2400000;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5f;
            NPC.value = Item.buyPrice(gold: 5);
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || partner != -1)
                return;

            foreach (NPC other in Main.npc)
            {
                if (other.active &&
                    other.whoAmI != NPC.whoAmI &&
                    other.ModNPC is BaseMartianMoon otherMoon &&
                    otherMoon.partner == -1)
                {
                    partner = other.whoAmI;
                    otherMoon.partner = NPC.whoAmI;
                    return;
                }
            }

            int playerSpawned = 1;
            if (source is EntitySource_BossSpawn spawn &&
                spawn.Target is Player bossSummoner)
            {
                playerSpawned = bossSummoner.whoAmI;
            }

            NPC.SpawnOnPlayer(playerSpawned, PartnerType);
        }


        public override void AI()
        {
            Player target = Main.player[NPC.target];

            if (!target.active || target.dead)
            {
                NPC.TargetClosest();
                target = Main.player[NPC.target];

                if (!target.active || target.dead)
                {
                    NPC.velocity.Y -= 0.2f;
                    NPC.timeLeft = 10;
                    return;
                }
            }

            NPC partner = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC candidate = Main.npc[i];
                if (candidate.active && candidate.type == PartnerType)
                {
                    partner = candidate;
                    break;
                }
            }

            bool phaseTwo = NPC.life < NPC.lifeMax / 2;
            float speed = phaseTwo ? 9f : 6f;
            float side = IsCharger ? -1f : 1f;
            Vector2 offset = new Vector2(side * 260f, -150f);
            Vector2 destination = target.Center + offset;

            if (partner != null && partner.active)
                destination += new Vector2(0f, IsCharger ? 80f : -60f);

            if (IsCharger)
            {
                NPC.ai[0]++;
                if (NPC.ai[0] >= (phaseTwo ? 100f : 150f))
                {
                    NPC.ai[0] = 0f;
                    Vector2 charge = target.Center - NPC.Center;
                    if (charge.LengthSquared() > 0f)
                        NPC.velocity = Vector2.Normalize(charge) * (phaseTwo ? 34f : 28f);
                }
                else
                {
                    MoveToward(destination, speed);
                }
            }
            else
            {
                MoveToward(destination, speed);

                NPC.ai[0]++;
                if (NPC.ai[0] >= (phaseTwo ? 40f : 65f))
                {
                    NPC.ai[0] = 0f;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 shot = target.Center - NPC.Center;
                        if (shot.LengthSquared() > 0f)
                            shot = Vector2.Normalize(shot) * (phaseTwo ? 10f : 7f);

                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center,
                            shot,
                            ProjectileID.PinkLaser,
                            phaseTwo ? 24 : 18,
                            0f,
                            Main.myPlayer
                        );
                    }
                }
            }

            NPC.rotation = NPC.velocity.X * 0.03f;
        }

        private void MoveToward(Vector2 destination, float speed)
        {
            Vector2 desired = destination - NPC.Center;
            if (desired.Length() > speed)
                desired = Vector2.Normalize(desired) * speed;

            NPC.velocity = (NPC.velocity * 24f + desired) / 25f;
        }
    }
}
