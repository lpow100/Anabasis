using Anabasis.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Bosses.PhobosAndDeimos
{
    [AutoloadBossHead]
    public class Deimos : BaseMartianMoon
    {
        protected override int PartnerType => ModContent.NPCType<Phobos>();

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
            float side = 1f;
            Vector2 offset = new Vector2(side * 260f, -150f);
            Vector2 destination = target.Center + offset;

            if (partner != null && partner.active)
                destination += new Vector2(0f,  -60f);

            MoveToward(destination, speed);

            NPC.ai[0]++;
            if (NPC.ai[0] >= (phaseTwo ? 22f : 30f))
            {
                NPC.ai[0] = 0f;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 shot = target.Center - NPC.Center;
                    if (shot.LengthSquared() > 0f)
                        shot = Vector2.Normalize(shot) * (phaseTwo ? 25f : 22f);

                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        NPC.Center,
                        shot,
                        ModContent.ProjectileType<MartianLaser>(),
                        phaseTwo ? 70 : 50,
                        5f,
                        Main.myPlayer
                    );
                }
            }

            NPC.rotation = NPC.velocity.X* 0.03f;
        } 
    }
}
