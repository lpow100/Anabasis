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
    public class Phobos : BaseMartianMoon
    {
        protected override int PartnerType => ModContent.NPCType<Deimos>();

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

            NPC partnerNPC = Main.npc[partner];

            bool phaseTwo = NPC.life < NPC.lifeMax / 2;
            float speed = phaseTwo ? 9f : 6f;
            float side = -1f;
            Vector2 offset = new Vector2(side * 260f, -150f);
            Vector2 destination = target.Center + offset;


            Vector2 charge = target.Center - NPC.Center;

            NPC.ai[0]++;
            if (NPC.ai[0] >= (phaseTwo ? 70f : 100f))
            {
                NPC.ai[0] = 0f;
                if (NPC.ai[1] < 8)
                {
                    if (charge.LengthSquared() > 0f)
                        NPC.velocity = Vector2.Normalize(charge) * (phaseTwo ? 32f : 28f);
                } else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 shot = target.Center - NPC.Center;
                        if (shot.LengthSquared() > 0f)
                            shot = Vector2.Normalize(shot) * (phaseTwo ? 21f : 18f);

                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center,
                            shot,
                            ModContent.ProjectileType<MartianFlame>(),
                            phaseTwo ? 90 : 70,
                            5f,
                            Main.myPlayer
                        );
                    }
                }
                NPC.ai[1]++;
                NPC.ai[1] = NPC.ai[1] % 16;
            }
            else
            {
                if (partnerNPC != null && partnerNPC.active)
                    destination += new Vector2(0f, 80f);

                MoveToward(destination, speed);
            }
        }
    }
}
