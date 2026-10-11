using Anabasis.Content.Projectiles;
using Anabasis.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Anabasis.Core.Systems
{
    public class AnabasisDashManager : ModSystem
    {
        public enum DashType
        {
            None, Basic, Invincible, Ram, Pounce
        }

        private static void MakeImmuneDuringDash(Player player, int dashDurationTicks, int extraImmuneTime)
        {
            player.immune = true;
            player.immuneTime = dashDurationTicks + extraImmuneTime;
        }

        public static void DashStart(Player player, DashType type, int dashDurationTicks, float dashSpeed, int dashCooldown, int dashDamage = 0)
        {
            DashPlayer DashPlayer = player.GetModPlayer<DashPlayer>();

            if (DashPlayer.dashDuration > 0 || player.dashDelay > 0)
                return;

            ForcedDashStart(player, type, dashDurationTicks, dashSpeed, dashCooldown, dashDamage);
        }

        public static void ForcedDashStart(Player player, DashType type, int dashDurationTicks, float dashSpeed, int dashCooldown, int dashDamage = 0)
        {
            DashPlayer DashPlayer = player.GetModPlayer<DashPlayer>();

            if (player.whoAmI != Main.myPlayer)
                return;

            if (player.mount.Active)
                return;

            player.dashType = 142;
            player.dashDelay = dashDurationTicks + dashCooldown;

            DashPlayer.currentDashType = type;
            DashPlayer.currentDashDamage = dashDamage;
            DashPlayer.dashDuration = dashDurationTicks;
            DashPlayer.dashCooldown = dashCooldown;

            Vector2 dashDirection = Main.MouseWorld - player.position;
                    dashDirection.Normalize();
                    dashDirection.Y *= 1.8f; // Gravity offset

            player.velocity = dashDirection * dashSpeed;
            DashPlayer.currentDashSpeed = player.velocity.X;

            if ((int)type > (int)DashType.Invincible)
                MakeImmuneDuringDash(player, dashDurationTicks, 95);
        }

        public static void UpdateDashInfo(Player player)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

            float oldDashDuration = dashPlayer.dashDuration;

            if (dashPlayer.dashDuration > 0) dashPlayer.dashDuration--;

            if (dashPlayer.dashDuration == 0 && oldDashDuration > 0)
            {
                player.dashType = 0;
                player.dashDelay = dashPlayer.dashCooldown;

                dashPlayer.currentDashDamage = 0;
                dashPlayer.currentDashSpeed = 0;
                dashPlayer.currentDashType = DashType.None;
                dashPlayer.pounceImpactTriggered = false;
                dashPlayer.damagedDuringDash.Clear();
            }
        }

        public static void UpdateDashes(Player player)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

            UpdateDashInfo(player);

            if (player.whoAmI != Main.myPlayer)
                return;

            if (dashPlayer.dashDuration <= 0 || player.mount.Active)
                return;

            player.dashType = 142;
            player.velocity.X = dashPlayer.currentDashSpeed;

            if (dashPlayer.currentDashType == DashType.Ram)
            {
                RamDashAttack(player);
            } 
            else if (dashPlayer.currentDashType == DashType.Pounce)
            {
                PounceDashAttack(player);
                dashPlayer.pounceImpactTriggered = true;
            }
        }

        private static void RamDashAttack(Player player)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

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
                    RamDashHitNPC(player, npc, hurtbox);
                }
            }
        }

        private static void RamDashHitNPC(Player player, NPC npc, Rectangle hurtbox)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

            if (hurtbox.Intersects(npc.getRect()) && (npc.noTileCollide || player.CanHit(npc)))
            {
                int dashDamage = (int)player.GetTotalDamage<BlitzDamageClass>().ApplyTo(dashPlayer.currentDashDamage);

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

        private static void PounceDashAttack(Player player)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

            if (dashPlayer.dashDuration == 1)
            {
                PounceDashHitNPC(player);
            }

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
                        PounceDashHitNPC(player);
                    }
                }
            }
        }

        private static void PounceDashHitNPC(Player player)
        {
            DashPlayer dashPlayer = player.GetModPlayer<DashPlayer>();

            int dashDamage = (int)player.GetTotalDamage<BlitzDamageClass>().ApplyTo(player.GetModPlayer<DashPlayer>().currentDashDamage);

            Projectile ram = Projectile.NewProjectileDirect(
                player.GetSource_FromThis(),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<AOEStrikeProjectile>(),
                dashDamage,
                4f,
                player.whoAmI
            );
            ram.DamageType = ModContent.GetInstance<BlitzDamageClass>();

            ForcedDashStart(player, DashType.Invincible, dashPlayer.dashDuration, -Math.Abs(dashPlayer.currentDashSpeed) * 4 / 5 , dashPlayer.currentDashDamage);
        }
    }
}
