using Anabasis.Content.Projectiles;
using Anabasis.Core.ModPlayers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

// Adapted from https://github.com/Fargo-Team/FargosSoulsMod/blob/226fadeadbe3422785a7708ba2cdf53bd8548c00/Core/Systems/DashManager.cs
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

            //if (player.dashType != 0 && player.dashType != 142)
            //    return;

            if (dashPlayer.dashDuration > 0) // already dashing - don't restart the timer
                return;

            if (player.dashDelay == 0 && !player.mount.Active)
            {
                const int dashLeaveWindow = 30;
                player.dashType = 142;
                dashPlayer.currentDashDamage = dashDamage; 

                Vector2 dashDir = Main.MouseWorld - player.position;
                dashDir.Normalize();
                dashDir.Y *= 1.8f; // Gravity is a bitch

                player.velocity = dashDir * dashSpeed;
                dashPlayer.currentDashSpeed = player.velocity.X;
                dashPlayer.dashDuration = dashDurationTicks;

                if ((int)type > 1) MakeImmuneDuringDash(player, dashDurationTicks, dashLeaveWindow);
                if (type == DashType.Ram)
                {
                    // Make the player deal damage??
                    // Maybe have the player spawn a hurtbox infront of it??
                }

                player.dashDelay = dashDurationTicks + dashLeaveWindow;
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
                    // Adapted from https://github.com/CalamityTeam/CalamityModPublic/blob/1a8cebd27ec5615316b78f71973446b5528d2b78/CalPlayer/CalamityPlayerDashEffects.cs
                    Rectangle hurtbox = new Rectangle((int)(player.position.X + player.velocity.X * 0.5 - 4f), (int)(player.position.Y + player.velocity.Y * 0.5 - 4), player.width + 8, player.height + 8);
                    foreach (NPC npc in Main.ActiveNPCs)
                    {
                        if (player.dontHurtCritters && NPCID.Sets.CountsAsCritter[npc.type])
                            continue;

                        if (!npc.dontTakeDamage && !npc.friendly)
                        {
                            if (hurtbox.Intersects(npc.getRect()) && (npc.noTileCollide || player.CanHit(npc)))
                            {
                                // Duplicated from the way TML edits vanilla ram dash damage (and Shield of Cthulhu)
                                int dashDamage = (int)player.GetTotalDamage<BlitzDamageClass>().ApplyTo(dashPlayer.currentDashDamage);

                                Projectile ram = Projectile.NewProjectileDirect(player.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), dashDamage, 0f, player.whoAmI, npc.whoAmI);
                                ram.DamageType = ModContent.GetInstance<BlitzDamageClass>();
                            }
                        }
                    }
                }
            }

        }
    }
}
