using Anabasis.Content.Projectiles;
using Anabasis.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Anabasis.Core.Systems
{
    public class AnabasisDashManager : ModSystem
    {
        public enum DashType
        {
            None, Basic, Invincible, Ram
        }

        private static void MakeImmuneDuringDash(Player player, int dashDurationTicks, int extraImmuneTime)
        {
            player.immune = true;
            player.immuneTime = dashDurationTicks + extraImmuneTime;
        }

        public static void DashStart(Player player, DashType type, int dashDurationTicks, float dashSpeed, int dashDamage = 0)
        {
            AnabasisPlayer dashPlayer = player.GetModPlayer<AnabasisPlayer>();

            if (player.whoAmI != Main.myPlayer)
                return;

            if (dashPlayer.dashDuration > 0) // Already dashing
                return;

            if (player.dashDelay == 0 && !player.mount.Active)
            {
                const int dashLeaveWindow = 30;
                player.dashType = 142;

                // Store state in dashPlayer
                dashPlayer.currentDashType = type; // FIX #1: Store the dash type!
                dashPlayer.currentDashDamage = dashDamage;

                Vector2 dashDir = Main.MouseWorld - player.position;
                dashDir.Normalize();
                dashDir.Y *= 1.8f; // Gravity offset

                player.velocity = dashDir * dashSpeed;
                dashPlayer.currentDashSpeed = player.velocity.X;
                dashPlayer.dashDuration = dashDurationTicks;

                if ((int)type > 1)
                    MakeImmuneDuringDash(player, dashDurationTicks, dashLeaveWindow);

                player.dashDelay = dashDurationTicks + dashLeaveWindow;

                dashPlayer.damagedDuringDash = new HashSet<int>();
            }
        }

        public static void UpdateDashes(Player player)
        {
            AnabasisPlayer dashPlayer = player.GetModPlayer<AnabasisPlayer>();

            if (player.whoAmI != Main.myPlayer)
                return;

            if (dashPlayer.dashDuration > 0 && !player.mount.Active)
            {
                player.dashType = 142;
                player.velocity.X = dashPlayer.currentDashSpeed;

                if (dashPlayer.currentDashType == DashType.Ram)
                {
                    Rectangle hurtbox = new Rectangle(
                        (int)(player.position.X + player.velocity.X * 0.5f - 4f),
                        (int)(player.position.Y + player.velocity.Y * 0.5f - 4f),
                        player.width + 8,
                        player.height + 8
                    );

                    foreach (NPC npc in Main.ActiveNPCs)
                    {
                        if (player.dontHurtCritters && NPCID.Sets.CountsAsCritter[npc.type])
                            continue;

                        if (dashPlayer.damagedDuringDash.Contains(npc.whoAmI))
                        {
                            continue;
                        }

                        if (!npc.dontTakeDamage && !npc.friendly)
                        {
                            if (hurtbox.Intersects(npc.getRect()) && (npc.noTileCollide || player.CanHit(npc)))
                            {
                                int dashDamage = (int)player.GetTotalDamage<BlitzDamageClass>().ApplyTo(dashPlayer.currentDashDamage);

                                // FIX #2: Ensure DirectStrike projectile hits target correctly and doesn't flood projectile limits
                                Projectile ram = Projectile.NewProjectileDirect(
                                    player.GetSource_FromThis(),
                                    npc.Center,
                                    Vector2.Zero,
                                    ModContent.ProjectileType<DirectStrike>(),
                                    dashDamage,
                                    0f,
                                    player.whoAmI,
                                    npc.whoAmI
                                );
                                ram.DamageType = ModContent.GetInstance<BlitzDamageClass>();

                                dashPlayer.damagedDuringDash.Add(npc.whoAmI);
                            }
                        }
                    }
                }
            }
        }
    }
}
