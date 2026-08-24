using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Anabasis.Common.DamageClasses;
using Anabasis.Content.Projectiles;

namespace Anabasis.Content.Items.Weapons.Alchemist
{
    public class PoisonedStick : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 18);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.damage = 6;
            Item.knockBack = 2.5f;
            Item.noMelee = true;
            Item.DamageType = ModContent.GetInstance<AlchemistDamageClass>();

            Item.shootSpeed = 7f;
            Item.shoot = ModContent.ProjectileType<PoisonedGlob>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddTile(TileID.WorkBenches)
                .AddCondition(Condition.InGraveyard)
                .Register();
        }
    }
}
