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

            return true;
        }
    }
}
