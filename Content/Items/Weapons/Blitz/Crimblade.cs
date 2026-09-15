using Anabasis.Core;
using Anabasis.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class Crimblade : ModItem
    {
        private const int DashCooldown = 120;
        private const int DashTime = 15;
        private const float DashSpeed = 9.5f;
        private const int DashDamage = 18;
        private const int MomentumCost = 15;

        public override string Texture => "Anabasis/Content/Items/Weapons/Blitz/BloodBlade";

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = DashCooldown;
            Item.damage = DashDamage;
            Item.DamageType = ModContent.GetInstance<BlitzDamageClass>();
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 30);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Ram, DashTime, DashSpeed, DashCooldown, DashDamage, MomentumCost);
            return true;
        }
    }
}
