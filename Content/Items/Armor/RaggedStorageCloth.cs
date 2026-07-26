using Anabasis.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class RaggedStorageCloth : ModItem
	{

		public override void SetDefaults() {
			Item.width = 30; // Width of the item
			Item.height = 20; // Height of the item
			Item.value = Item.sellPrice(silver: 2); // How many coins the item is worth
			Item.rare = ItemRarityID.White; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			player.GetModPlayer<AlchemistPlayer>().consumableSaveChance += 0.10f;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
                .AddIngredient(ItemID.Silk, 20)
                .AddIngredient(ItemID.Bottle, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}