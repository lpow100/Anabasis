using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Anabasis.Content.Buffs;
using Anabasis.Core.Systems;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class CopperGauntlet : ModItem
    {
        const int dashTime = 12;
        const int dashCooldown = 360;
        const float dashSpeed = 6.5f;
        const int dashDamage = 8;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 14;
            Item.useTime = 14;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 3, copper: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 12)
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
