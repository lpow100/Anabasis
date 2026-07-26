using Anabasis.Common.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons
{
    public class BloodSyringe : ModItem
    {
        public override void SetDefaults()
        {
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

            // Weapon Properties			
            Item.damage = 16;
            Item.knockBack = 5f;
            Item.noUseGraphic = true; // The item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            // Projectile Properties
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<Projectiles.BloodSyringeProjectile>();
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient(ItemID.IronBar, 12)
                .AddIngredient(ItemID.LesserHealingPotion, 2)
                .AddTile(TileID.Bottles)
                .Register();
            CreateRecipe(4)
                .AddIngredient(ItemID.LeadBar, 12)
                .AddIngredient(ItemID.LesserHealingPotion, 2)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
