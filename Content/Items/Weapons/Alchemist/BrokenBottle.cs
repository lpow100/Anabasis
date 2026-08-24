using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class BrokenBottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 1);
            Item.maxStack = Item.CommonMaxStack;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 21;
            Item.useTime = 21;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.damage = 15;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<GlassShards>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddIngredient(ItemID.Bottle, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
