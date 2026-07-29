using Anabasis.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Common
{
    public class RemoveHellforgeSpawns : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = Main.maxTilesY - 200; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];

                    if (tile.HasTile && tile.TileType == TileID.Hellforge
                        && tile.TileFrameX == 0 && tile.TileFrameY == 0)
                    {
                        WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: true);
                        WorldGen.PlaceTile(x, y, TileID.Statues, mute: true, forced: true, style: 49);
                    }
                }
            }
        }
    }

    public class HellforgeRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.Hellforge);
            recipe.AddIngredient(ItemID.Hellstone, 15);
            recipe.AddIngredient(ModContent.ItemType<Fulgurite>(), 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}