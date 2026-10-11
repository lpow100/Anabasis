using Anabasis.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.GameContent.Bestiary.IL_BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;

namespace Anabasis.Content.Items.Placeable
{
    public class SunkenBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.SunkenBrick>());
            Item.width = 16;
            Item.height = 16;
        }
    }
}