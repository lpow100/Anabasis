using Anabasis.Content.Items;
using Anabasis.Core;
using Anabasis.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class SuperSoftGlove : ModItem
    {
        private const int DashCooldown = 204;
        private const int DashTime = 19;
        private const float DashSpeed = 13f;
        private const int DashDamage = 9;
        private const int MomentumCost = 30;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 19;
            Item.useTime = DashCooldown;
            Item.damage = DashDamage;
            Item.DamageType = ModContent.GetInstance<BlitzDamageClass>();
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 55);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<DeerPelt>(8)
                .AddIngredient(ItemID.Leather, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Pounce, DashTime, DashSpeed, DashCooldown, DashDamage, MomentumCost);
            return true;
        }
    }
}
