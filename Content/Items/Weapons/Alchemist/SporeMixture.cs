using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class SporeMixture : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 40);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 35;
            Item.knockBack = 3f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<SporeMixtureProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.GlowingMushroom, 3)
                .AddIngredient(ItemID.TissueSample, 1)
                .AddTile(TileID.WorkBenches)
                .Register();

            CreateRecipe(5)
                .AddIngredient(ItemID.GlowingMushroom, 3)
                .AddIngredient(ItemID.ShadowScale, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
