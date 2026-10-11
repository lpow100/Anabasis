using Anabasis.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.Bestiary.IL_BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;

namespace Anabasis.Content.Items.Placeable
{
    public class SunkenWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableWall(ModContent.WallType<Tiles.Walls.SunkenWall>());
            Item.width = 16;
            Item.height = 16;
        }
    }
}