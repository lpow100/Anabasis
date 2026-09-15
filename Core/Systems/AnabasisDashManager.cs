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

        private static void GainMomentum(Player player)
        {
            BlitzPlayer blitzPlayer = player.GetModPlayer<BlitzPlayer>();
            if (blitzPlayer.IsDashing) return;


            if (Main.LocalPlayer.HeldItem.DamageType != ModContent.GetInstance<BlitzDamageClass>())
            {
                blitzPlayer.momentum -= 1f / 6f;
                if (blitzPlayer.momentum < 0) blitzPlayer.momentum = 0;

                return;
            }

            double maxMomentumGain = blitzPlayer.maxMomentum / 8 * 7;

            double speedSquared = player.velocity.LengthSquared();

            double momentumGain = 0.0;

            if (speedSquared > 0.0)
            {
                double speed = Math.Sqrt(speedSquared);

                momentumGain = maxMomentumGain *
                               (1.0 - Math.Pow(2.0, -speed / 55.0));
            }

            blitzPlayer.momentum += (float)momentumGain / 60;

            if (blitzPlayer.momentum > blitzPlayer.maxMomentum) blitzPlayer.momentum = blitzPlayer.maxMomentum;
        }

        public static bool DashStart(Player player, DashType type, int dashDurationTicks, float dashSpeed, int dashCooldown, int dashDamage = 0, int momentumCost = 0)
        {
            BlitzPlayer blitzPlayer = player.GetModPlayer<BlitzPlayer>();

            if (blitzPlayer.momentum <= momentumCost || blitzPlayer.dashDuration > 0 || player.dashDelay > 0)
                return false;

            return ForcedDashStart(player, type, dashDurationTicks, dashSpeed, dashCooldown, dashDamage, momentumCost);
        }

        public static bool ForcedDashStart(Player player, DashType type, int dashDurationTicks, float dashSpeed, int dashCooldown, int dashDamage = 0, int momentumCost = 0)
        {
            BlitzPlayer blitzPlayer = player.GetModPlayer<BlitzPlayer>();

            if (player.whoAmI != Main.myPlayer)
                return false;

            if (player.mount.Active)
                return false;

            player.dashType = 142;
            player.dashDelay = dashDurationTicks + dashCooldown;

            blitzPlayer.currentDashType = type;
            blitzPlayer.currentDashDamage = dashDamage;
            blitzPlayer.dashDuration = dashDurationTicks;
            blitzPlayer.dashCooldown = dashCooldown;

           Vector2 dashDir = Main.MouseWorld - player.position;
            dashDir.Normalize();
            dashDir.Y *= 1.8f; // Gravity offset

            player.velocity = dashDir * dashSpeed;
            blitzPlayer.currentDashSpeed = player.velocity.X;

            if ((int)type > (int)DashType.Invincible)
                MakeImmuneDuringDash(player, dashDurationTicks, 95);

            blitzPlayer.momentum -= momentumCost;
            return true;
        }

        public static void UpdateDashInfo(Player player)
        {
            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

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
            GainMomentum(player);

            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

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
            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

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
            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

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
            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

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
            BlitzPlayer dashPlayer = player.GetModPlayer<BlitzPlayer>();

            int dashDamage = (int)player.GetTotalDamage<BlitzDamageClass>().ApplyTo(player.GetModPlayer<BlitzPlayer>().currentDashDamage);

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

            ForcedDashStart(player, DashType.Invincible, dashPlayer.dashDuration / 4, -Math.Abs(dashPlayer.currentDashSpeed), dashPlayer.currentDashDamage);
        }
    }
}
