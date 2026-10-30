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
            Vector2 shootOffset = new Vector2(side * 260f, -150f);
            Vector2 charge = target.Center - NPC.Center;

            Timer ++;

            if (Attack == 1)
            {
                if (Timer >= (phaseTwo ? 100f : 140f))
                {
                    Timer = 0f;
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
                else
                {
                    if (partnerNPC != null && partnerNPC.active)
                        shootOffset += new Vector2(0f, 80f);

                    MoveToward(target.Center + shootOffset, speed);
                }
            } else
            {
                float dashInterval = phaseTwo ? 70f : 100f;

                if (Timer >= dashInterval)
                {
                    Timer = 0f;

                    float dashSpeed = phaseTwo ? 33f : 29f;
                    float chargeLengthSquared = charge.LengthSquared();

                    if (chargeLengthSquared > 0.001f)
                    {
                        NPC.velocity = charge * (dashSpeed / MathF.Sqrt(chargeLengthSquared));
                        followingAfterDash = false;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (NPC.velocity.LengthSquared() <= 3f && !followingAfterDash)
                    {
                        followingAfterDash = true;
                        lastDashOffset = charge;
                    } else if (followingAfterDash)
                    {
                        MoveToward(lastDashOffset, speed);
                    }
                }
            }
        }
    }
}
