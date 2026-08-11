using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Anabasis.Content.Bosses.GiantEel
{
    // Attach in GiantEelHead.SetDefaults() with:
    //   NPC.BossBar = ModContent.GetInstance<GiantEelBossBar>();
    public class GiantEelBossBar : ModBossBar
    {
        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            // Swap this for your own boss head icon once you have one registered.
            // Placeholder: reuses a vanilla head so it compiles out of the box.
            return TextureAssets.NpcHead[36];
        }

        public override bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
        {
            float lifePercent = drawParams.Life / drawParams.LifeMax;

            // Gentle vertical bob, like the bar is floating on water.
            float bob = (float)System.Math.Sin(Main.GameUpdateCount * 0.05f) * 3f;
            drawParams.BarCenter.Y += bob;

            // Sharp, brief shake only when it's mid-lunge - a nice tie-in with the
            // head's AttackState (ai[1] == 2 while lunging), rather than a generic
            // "low health" shake. Falls back to a low-health shake if not lunging.
            bool isLunging = npc.ai[1] == 2f;
            float shakeIntensity = isLunging
                ? 1f
                : Utils.Clamp(1f - lifePercent - 0.2f, 0f, 1f);

            drawParams.BarCenter += Main.rand.NextVector2Circular(0.5f, 0.5f) * shakeIntensity * (isLunging ? 6f : 12f);

            // Icon color shifts from a calm sea-blue toward a stormy teal/white flash
            // as health drops, instead of full rainbow disco cycling.
            Color calm = new Color(60, 130, 200);
            Color stormy = new Color(210, 230, 235);
            drawParams.IconColor = Color.Lerp(stormy, calm, lifePercent);

            return true;
        }
    }
}
