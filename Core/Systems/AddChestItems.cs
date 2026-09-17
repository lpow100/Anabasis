using Anabasis.Content.Items.Weapons.Blitz.Gauntlets;
using rail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Core.Systems
{
	// This class showcases adding additional items to vanilla chests.
	// This example simply adds additional items. More complex logic would likely be required for other scenarios.
	// If this code is confusing, please learn about "for loops" and the "continue" and "break" keywords: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements
	public class AddChestItems : ModSystem
	{
        private void AddSecndItemToChest(int item, Chest chest)
        {
            for (int inventoryIndex = 1; inventoryIndex < Chest.maxItems + 1; inventoryIndex++) {
                chest.item[inventoryIndex + 1].SetDefaults(chest.item[inventoryIndex].type);
            }
            chest.item[1].SetDefaults(item);
        }
		public override void PostWorldGen() 
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++) {
				Chest chest = Main.chest[chestIndex];
				if (chest == null) {
					continue;
				}
				Tile chestTile = Main.tile[chest.x, chest.y];
                // Dungeon chest
				if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 2 * 36) {
					if (WorldGen.genRand.NextBool(5))
						continue;
                    AddSecndItemToChest(ModContent.ItemType<BlueKnuckles>(),chest);
                }
            }
        }

    }
}