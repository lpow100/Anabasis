using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Anabasis.Content.Buffs;
using Anabasis.Core.Systems;
using Anabasis.Core;

namespace Anabasis.Content.Items.Weapons.Blitz.Gauntlets
{
    public class GemKnuckles : ModItem
    {
        const int dashCooldown = 60;
        const int dashTime = 16;
        const float dashSpeed = 10.5f;
        const int dashDamage = 45;
        const int momentumCost = 15;

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
                .AddIngredient(ItemID.StoneBlock, 80)
                .AddRecipeGroup("Anabasis:AnyGem", 5)
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
