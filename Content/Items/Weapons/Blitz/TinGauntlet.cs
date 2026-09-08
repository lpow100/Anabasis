using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Anabasis.Content.Buffs;
using Anabasis.Core.Systems;
using Anabasis.Core;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class TinGauntlet : ModItem
    {
        const int dashCooldown = 75;
        const int dashTime = 14;
        const float dashSpeed = 8.5f;
        const int dashDamage = 30;
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
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 6, copper: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TinBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Pounce, dashTime, dashSpeed, dashCooldown, dashDamage, momentumCost);
            return base.UseItem(player);
        }
    }
}
