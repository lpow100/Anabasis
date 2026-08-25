using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class ElectronicsJar : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 70);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 29;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<ElectronicsJarProjectile>();
        }

        public override void AddRecipes()
        {
             CreateRecipe(14)
                 .AddIngredient(ItemID.Wire, 8)
                 .AddRecipeGroup(RecipeGroupID.IronBar, 4)
                 .AddIngredient(ItemID.Bottle, 10)
                 .AddTile(TileID.Anvils)
                 .Register();
        }
    }
}
