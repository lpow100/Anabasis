using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons
{
	public class StingerPole : ModItem
	{

		public override void SetDefaults() {
			// Common Properties
			Item.rare = ItemRarityID.Blue; // Assign this item a rarity level of Pink
			Item.value = Item.sellPrice(silver: 10); // The number and type of coins item can be sold for to an NPC

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.useAnimation = 22; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useTime = 22; // The length of the item's use time in ticks (60 ticks == 1 second.)
			Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
			Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
            Item.consumable = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.width = 38;
            Item.height = 38;

			// Weapon Properties
			Item.damage = 12;
			Item.knockBack = 6.5f;
			Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
			Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();
			Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

			// Projectile Properties
			Item.shootSpeed = 6f; // The speed of the projectile measured in pixels per frame.
			Item.shoot = ModContent.ProjectileType<StingerPoleProjectile>(); // The projectile that is fired from this weapon
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe(5)
				.AddIngredient(ItemID.PlatinumBar)
				.AddIngredient(ItemID.JungleSpores)
				.AddIngredient(ItemID.Stinger)
				.Register();
		}
	}
}