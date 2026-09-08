using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Anabasis.Content.Buffs;
using Anabasis.Core.Systems;
using Anabasis.Core;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class WoodenThorn : ModItem
    {
        const int dashCooldown = 60;
        const int dashTime = 14;
        const float dashSpeed = 10f;
        const int dashDamage = 7;
        const int momentumCost = 10;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 14;
            Item.useTime = dashCooldown;
            Item.damage = dashDamage;
            Item.DamageType = ModContent.GetInstance<BlitzDamageClass>();
            Item.noMelee = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(copper: 90);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 30)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Ram, dashTime, dashSpeed, dashCooldown, dashDamage, momentumCost);
            return base.UseItem(player);
        }
    }
}
