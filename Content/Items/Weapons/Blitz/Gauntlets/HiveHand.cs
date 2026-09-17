using Anabasis.Content.Buffs;
using Anabasis.Core;
using Anabasis.Core.ModPlayers;
using Anabasis.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Anabasis.Content.Items.Weapons.Blitz.Gauntlets
{
    public class HiveHand : ModItem
    {
        const int dashCooldown = 50;
        const int dashTime = 17;
        const float dashSpeed = 10.75f;
        const int dashDamage = 75;
        const int momentumCost = 25;

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
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 75);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BeeWax, 14)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            int beeCount = Main.rand.Next(5,7);

            BlitzPlayer blitzPlayer = player.GetModPlayer<BlitzPlayer>();
            if (blitzPlayer.momentum <= momentumCost || blitzPlayer.dashDuration > 0 || player.dashDelay > 0)
                beeCount = 0;

            for (int i = 0; i < beeCount; i++)
            {
                Vector2 beeDir = new(
                    Main.rand.NextFloatDirection() * 5,
                    Main.rand.NextFloatDirection() * 5
                );
                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    player.position,
                    beeDir,
                    ProjectileID.Bee,
                    dashDamage / beeCount,
                    3.4f
                );
            }
            AnabasisDashManager.DashStart(player, AnabasisDashManager.DashType.Pounce, dashTime, dashSpeed, dashCooldown, dashDamage, momentumCost);
            return base.UseItem(player);
        }
    }
}
