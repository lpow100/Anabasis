using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons
{
	public class ExplosiveVial : ModItem
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Type] = true;
			Item.ResearchUnlockCount = 99;
		}

		public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shootSpeed = 6f;
			Item.shoot = ModContent.ProjectileType<Projectiles.ExplosiveVialProjectile>();
            Item.damage = 16;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();
			Item.width = 20;
			Item.height = 24;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.UseSound = SoundID.Item1;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.value = Item.buyPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Blue;
		}

		public override void AddRecipes() {
			CreateRecipe(3)
				.AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.IronOre, 5)
                .AddIngredient(ItemID.Bomb)
				.AddTile(TileID.Bottles)
				.Register();
			CreateRecipe(3)
				.AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.LeadOre, 5)
                .AddIngredient(ItemID.Bomb)
				.AddTile(TileID.Bottles)
				.Register();
		}
	}
}