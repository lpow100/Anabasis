using Anabasis.Core;
using Anabasis.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz
{
    public class HiveHand : ModItem
    {
        private const int DashCooldown = 234;
        private const int DashTime = 18;
        private const float DashSpeed = 12f;
        private const int DashDamage = 9;
        private const int MomentumCost = 28;
        private const int BeeDamage = 9;

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
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
                .AddIngredient(ItemID.Beeswax, 10)
                .AddIngredient(ItemID.BottledHoney, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (!AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Pounce, DashTime, DashSpeed, DashCooldown, DashDamage, MomentumCost))
                return false;

            Vector2 direction = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
            for (int i = 0; i < 2; i++)
            {
                Projectile bee = Projectile.NewProjectileDirect(
                    player.GetSource_ItemUse(Item),
                    player.Center,
                    direction.RotatedByRandom(MathHelper.ToRadians(12f)) * 8f,
                    ProjectileID.Bee,
                    BeeDamage,
                    1f,
                    player.whoAmI
                );
                bee.DamageType = ModContent.GetInstance<BlitzDamageClass>();
            }

            return true;
        }
    }
}
