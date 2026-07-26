using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Anabasis.Common.DamageClasses;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class PosionedStick : ModItem
	{
		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 28;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.autoReuse = true;

			Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();
			Item.damage = 3;
			Item.knockBack = 6;
            Item.noMelee = true;

			Item.value = Item.buyPrice(copper: 75);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;

			Item.shoot = ModContent.ProjectileType<PoisonedGlob>(); // ID of the projectiles the sword will shoot
			Item.shootSpeed = 6f; // Speed of the projectiles the sword will shoot

			// If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
			// Item.attackSpeedOnlyAffectsWeaponAnimation = true;

			// Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
			// Item.ChangePlayerDirectionOnShoot = false;
		}
		// This method gets called when firing your weapon/sword.
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
				Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);

			return false; // Return false because we don't want tModLoader to shoot projectile
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Wood, 20)
				.AddTile(TileID.WorkBenches)
                .AddCondition(Condition.InGraveyard)
				.Register();
		}
	}
}
