using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class ExplosiveVial : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(copper: 110);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 22;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 12.5f;
            Item.shoot = ModContent.ProjectileType<ExplosiveVialProjectile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(20)
                .AddIngredient(ItemID.Demonite)
                .AddIngredient(ItemID.Bomb, 2)
                .AddIngredient(ItemID.Bottle, 10)
                .AddTile(TileID.WorkBenches)
                .Register();

            CreateRecipe(20)
                .AddIngredient(ItemID.Crimtane)
                .AddIngredient(ItemID.Bomb, 2)
                .AddIngredient(ItemID.Bottle, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
