using Anabasis.Core.ModPlayers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using Terraria;
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

        public static void DashStart(Player player, int dashDurationTicks)
        {
            AnabasisPlayer dashPlayer = player.GetModPlayer<AnabasisPlayer>();

            if (player.whoAmI != Main.myPlayer)
                return;

            //if (player.dashType != 0 && player.dashType != 142)
            //    return;

            if (dashPlayer.dashDuration > 0) // already dashing - don't restart the timer
                return;

            Main.NewText("Dash Delay is: " + player.dashDelay);

            if (player.dashDelay == 0 && !player.mount.Active)
            {

                player.dashType = 142;
                player.velocity.X = player.direction * 7.0f;
                dashPlayer.currentDashSpeed = player.velocity.X;
                dashPlayer.dashDuration = dashDurationTicks;

                player.immune = true;
                player.immuneTime = dashDurationTicks + 45;

                player.dashDelay = player.immuneTime + 45;
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
            }

        }
    }
}