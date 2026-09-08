using Anabasis.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Anabasis.Core.ModPlayers
{
    public class BlitzPlayer : ModPlayer
    {
        public int dashDuration;
        public bool IsDashing
        {
            get => dashDuration > 0;
        }
        public float currentDashSpeed;
        public AnabasisDashManager.DashType currentDashType;
        public int currentDashDamage;
        public HashSet<int> damagedDuringDash = [];
        public bool pounceImpactTriggered;
        public int dashCooldown;

        public float momentum;
        public float maxMomentum = 100.0f;

        public override void PostUpdate()
        {
            AnabasisDashManager.UpdateDashes(Player);

            if (maxMomentum <= 0f)
                return;

            float percentage = MathHelper.Clamp(
                momentum / maxMomentum,
                0f,
                1f
            );

            // Convert the player's world position to screen position
            Vector2 screenPosition = Player.Bottom - Main.screenPosition;

            // Position the bar below the player
            screenPosition.Y += 6f;

            float barWidth = 40f;
            float barHeight = 5f;

            Rectangle background = new Rectangle(
                (int)(screenPosition.X - barWidth / 2f),
                (int)screenPosition.Y,
                (int)barWidth,
                (int)barHeight
            );

            Rectangle fill = new Rectangle(
                background.X,
                background.Y,
                (int)(barWidth * percentage),
                (int)barHeight
            );

            Texture2D pixel = TextureAssets.MagicPixel.Value;

            Main.spriteBatch.Begin();

            // Background
            Main.spriteBatch.Draw(
                pixel,
                background,
                Color.Black * 0.8f
            );

            // Filled portion
            Main.spriteBatch.Draw(
                pixel,
                fill,
                Color.Cyan
            );

            Main.spriteBatch.End();
        }
    }
}
