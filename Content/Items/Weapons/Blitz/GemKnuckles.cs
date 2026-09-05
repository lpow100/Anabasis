using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Anabasis.Content.Buffs;
using Anabasis.Core.Systems;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class GemKnuckles : ModItem
    {
        const int dashTime = 7;
        const int dashCooldown = 5 * 60;
        const float dashSpeed = 8.5f;
        const int dashDamage = 30;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 14;
            Item.useTime = 14;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 6, copper: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 10)
                .AddRecipeGroup("Anabasis:AnyGem", 5)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddRecipeGroup("Anabasis:AnyGem", 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff<ShockedDebuff>();
        }

        public override bool? UseItem(Player player)
        {
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Ram, dashTime, dashSpeed, dashDamage);
            player.AddBuff(ModContent.BuffType<ShockedDebuff>(), dashTime + dashCooldown);
            return base.UseItem(player);
        }
    }
}
