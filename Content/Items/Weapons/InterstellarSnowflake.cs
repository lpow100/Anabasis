using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons
{
	public class InterstellarSnowflake : ModItem
	{
		public override void SetDefaults() {
			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

			// Common Properties
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 10);
			Item.maxStack = Item.CommonMaxStack;

			// Use Properties
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
            Item.width = 14;
            Item.height = 14;

			// Weapon Properties			
			Item.damage = 14;
			Item.knockBack = 5f;
			Item.noUseGraphic = true; // The item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

			// Projectile Properties
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<Projectiles.PainfulPocketSandProjectile>(); // The projectile that will be thrown
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe(6)
				.AddIngredient(ItemID.SandBlock, 15)
				.AddIngredient(ItemID.AntlionMandible)
				.AddIngredient(ItemID.FossilOre, 10)
                .AddTile(TileID.Anvils)
				.Register();
		}
	}
}