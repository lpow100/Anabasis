using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class CoralAmalgamation : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(70);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 12;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<CoralAmalgamationProjectile>();
        }

        // TODO: Make this with sea stonea
       /* public override void AddRecipes()
        {
            CreateRecipe(12)
                .AddIngredient(ItemID.CoralstoneBlock, 1)
                .AddIngredient(ItemID.ShellPileBlock, 3)
                .AddIngredient(ItemID.TissueSample, 2)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe(12)
                .AddIngredient(ItemID.CoralstoneBlock, 1)
                .AddIngredient(ItemID.ShellPileBlock, 3)
                .AddIngredient(ItemID.ShadowScale, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }*/
    }
}
